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
        public async Task Add_PassCorrectData_ShouldBeOk() 
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UsersListDatabase")
                .Options;

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
        public void GetAll_CallingWithoutParameters_ShouldReturnUsersList() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "UsersListDatabase")
                .Options;
            var dbContext = new AppDBContext(options);

            var sut = new UserRepository(dbContext);

            sut.Add(new User { Id = 100, Email = "test1@gmail.com", PasswordHash = "testUser1HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-8) });
            sut.Add(new User { Id = 200, Email = "test2@gmail.com", PasswordHash = "testUser2HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-7) });
            sut.Add(new User { Id = 300, Email = "test3@gmail.com", PasswordHash = "testUser3HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-6) });
            

            //Act
            List<User> users = sut.GetAll();

            //Assert
            
            var userId100 = users.Any(u => u.Id == 100 && u.Email == "test1@gmail.com");
            var userId200 = users.Any(u => u.Id == 200 && u.Email == "test2@gmail.com");
            var userId300 = users.Any(u => u.Id == 300 && u.Email == "test3@gmail.com");

            Assert.IsTrue(userId100);
            Assert.IsTrue(userId200);
            Assert.IsTrue(userId300);            
        }

        [Test]
        public void GetById_CallingWithoutParameters_ShouldBeUserObject()
        {
            //Arrange
            var user = new User() 
            {
                Id = 101,
                Email = "test@test.com",
                PasswordHash = "kjbn3465bkjn3n4kj345jbnkj346hjkh",
                AddedDate = DateTime.UtcNow.AddMinutes(-2)                
            };

            Mock<IUserRepository> mock = new Mock<IUserRepository>();

            //Act
            mock.Setup(m => m.Get(1)).Returns(user);

            //Assert
            Assert.NotNull(mock.Object.Get(1));
            Assert.That(user.Email, Is.EqualTo("test@test.com"));
        }

        [Test]
        public void Update_PassObjectWithUpdatedUserEmail_ShouldUpdatedEmailOk()
        {
            //Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            
            var user = new User()
            {
                Id = 102,
                Email = "test@test.com",
                PasswordHash = "kjbn3465bkjn3n4kj345jbnkj346hjkh",
                AddedDate = DateTime.UtcNow.AddMinutes(-2)                
            };

            var expectedUser = new User
            {
                Id = 102,
                Email = "test@updatedTest.com",
                PasswordHash = "kjbn3465bkjn3n4kj345jbnkj346hjkh"                                
            };

            //Act
            
            mock.Setup(u => u.Update(expectedUser));

            var result = mock.SetupGet(u => u.GetByEmail("test@updatedTest.com"));
            //Assert
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<User>());
            Assert.AreSame(result, expectedUser);
        }
    }
}
