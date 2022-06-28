using CostApplication.Data;
using CostApplication.Models;
using CostApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApplication.Repositories;

namespace CostApplication.Tests.Repositories
{
    [TestFixture]
    class UserRepositoryTests
    {
        [Test]
        public async Task Add() 
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UsersListDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDBContext(options))
            {
                var repo = new UserRepository(context);
                repo.Add(new User
                {
                    Email = "test@gmail.com",
                    PasswordHash = "brrm"
                });
                var users = await context.Users.ToListAsync();
                var user = users.First();

                //Assert
                Assert.That(users.Count, Is.EqualTo(1));
                Assert.That(user.Id, Is.GreaterThanOrEqualTo(1));
                Assert.That(user.Email, Is.EqualTo("test@gmail.com"));
                Assert.That(user.AddedDate, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-3)));
            }            
        }

        [Test]
        public void GetAll_Returns_UserList() 
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UsersListDatabase")
                .Options;

            using (var context = new AppDBContext(options))
            {
                
                var repo = new UserRepository(context);

                repo.Add(new User { Id = 100, Email = "test1@gmail.com", PasswordHash = "testUser1HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-8) });
                repo.Add(new User { Id = 200, Email = "test2@gmail.com", PasswordHash = "testUser2HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-7) });
                repo.Add(new User { Id = 300, Email = "test3@gmail.com", PasswordHash = "testUser3HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-6) });
                repo.Add(new User { Id = 400, Email = "test4@gmail.com", PasswordHash = "testUser4HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-5) });


                List<User> users = repo.GetAll().ToList();

                Assert.That(users.Count, Is.EqualTo(4));
            }
        }

        [Test]
        public void GetById_Return_User()
        {
            var user = new User() 
            {
                Id = 101,
                Email = "test@test.com",
                PasswordHash = "kjbn3465bkjn3n4kj345jbnkj346hjkh",
                AddedDate = DateTime.UtcNow.AddMinutes(-2)                
            };

            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Get(1)).Returns(user);

            Assert.NotNull(mock.Object.Get(1));
            Assert.That(user.Email, Is.EqualTo("test@test.com"));
        }

        [Test]
        public void Update_ReturnUserWithUpdatedEmail_Success()
        {
            //Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            Mock<IAppDBContext> context = new Mock<IAppDBContext>();
            var user = new User()
            {
                Id = 1,
                Email = "test@test.com",
                PasswordHash = "kjbn3465bkjn3n4kj345jbnkj346hjkh",
                AddedDate = DateTime.UtcNow.AddMinutes(-2)                
            };

            var expectedUser = new User
            {
                Id = 1,
                Email = "test@updatedTest.com",
                PasswordHash = "kjbn3465bkjn3n4kj345jbnkj346hjkh"                                
            };


            //Act
            context.Object.Users.Add(user);
            //mock.Setup(u => u.Add(user));
            mock.Setup(u => u.Update(expectedUser));


            var result = mock.SetupGet(u => u.GetByEmail("test@updatedTest.com"));
            //Assert
            //Assert.AreNotEqual(mock.Object.GetByEmail("test@test.com"), user.Email);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<User>());
            Assert.AreSame(result, expectedUser);
        }
    }
}
