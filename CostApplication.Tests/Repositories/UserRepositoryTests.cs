using CostApplication.Data;
using CostApplication.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UserApplication.Repositories;

namespace CostApplication.Tests.Repositories
{
    [TestFixture]
    class UserRepositoryTests
    {
        private DbSet<T> CreateDbSet<T>(IQueryable<T> collection) where T : class
        {
            var stubDbSet = new Mock<DbSet<T>>();
            stubDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(collection.Provider);
            stubDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(collection.Expression);
            stubDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(collection.ElementType);
            stubDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(collection.GetEnumerator());
            return stubDbSet.Object;
        }

        [Test]
        public void Add_PassCorrectData_ShouldBeOk() 
        {
            //Arange
            var collection = new List<User>{};

            var userDBSet = CreateDbSet(collection.AsQueryable());
            var stubContext = new Mock<IAppDBContext>();
            stubContext.Setup(u => u.Users).Returns(userDBSet);

            var sut = new UserRepository(stubContext.Object);
            var user = new User
            {
                Email = "test@gmail.com",
                PasswordHash = "brrm"
            };
            
            //Act
            var result = sut.Add(user);
            
            //Assert
            Assert.AreEqual(result.Email, "test@gmail.com");
            
        }

        [Test]
        public void GetAll_CallingWithoutParameters_ShouldReturnUsersList()
        {
            //Arrange                        

            var expected = new List<User>
            {
                new User { Email = "test1@gmail.com", PasswordHash = "testUser1HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-8) },
                new User { Email = "test2@gmail.com", PasswordHash = "testUser2HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-7) },
                new User { Email = "test3@gmail.com", PasswordHash = "testUser3HashedPassword", AddedDate = DateTime.UtcNow.AddMinutes(-6) }
            };

            var userDbSet = CreateDbSet(expected.AsQueryable());
            var stubContext = new Mock<IAppDBContext>();
            stubContext.Setup(x => x.Users).Returns(userDbSet);

            var sut = new UserRepository(stubContext.Object);
            //Act
            var actual = (List<User>)sut.GetAll();

            //Assert
            CollectionAssert.AreEquivalent(expected, actual);                        
        }

        [Test]
        public void GetById_CallingWithoutParameters_ShouldBeUserObject()
        {
            //Arrange
            var expected = new User {Email = "test@expected.com"};
            var collection = new List<User>
            {
                new User { Id = 1, Email = "test1@gmail.com"},
                new User { Id = 2, Email = "test@expected.com"},
                new User { Id = 3, Email = "test3@gmail.com"}
            };

            var userDbSet = CreateDbSet(collection.AsQueryable());
            var stubContext = new Mock<IAppDBContext>();
            stubContext.Setup(x => x.Users).Returns(userDbSet);

            var sut = new UserRepository(stubContext.Object);

            //Act
            var actual = sut.Get(2);

            //Assert
            Assert.AreEqual(expected.Email, actual.Email);
        }

        [Test]
        public void Update_PassObjectWithUpdatedUserEmail_ShouldUpdatedEmailOk()
        {
            //Arrange            
            var collection = new List<User>
            {
                new User { Id = 1, Email = "test1@gmail.com", PasswordHash = "testUser1HashedPassword"},
                new User { Id = 2, Email = "test2@gmail.com", PasswordHash = "testUser2HashedPassword"},
                new User { Id = 3, Email = "test3@gmail.com", PasswordHash = "testUser3HashedPassword"}
            };            
                
            var userDbSet = CreateDbSet(collection.AsQueryable());
            var stubContext = new Mock<IAppDBContext>();
            stubContext.Setup(x => x.Users).Returns(userDbSet);

            var sut = new UserRepository(stubContext.Object);

            //Act
            var userToUpdate = sut.Get(2);
            var result = sut.Update(userToUpdate);
            //Assert
            Assert.AreEqual(result, userToUpdate);

        }

        [Test]
        public void GetByEmail_PassCorrectEmail_ReturnUser_ShouldBeOk()
        {
            //Arrange            
            var collection = new List<User>
            {
                new User { Id = 1, Email = "test1@gmail.com", PasswordHash = "testUser1HashedPassword"},
                new User { Id = 2, Email = "test2@gmail.com", PasswordHash = "testUser2HashedPassword"},
                new User { Id = 3, Email = "test3@gmail.com", PasswordHash = "testUser3HashedPassword"}
            };

            var userDbSet = CreateDbSet(collection.AsQueryable());
            var stubContext = new Mock<IAppDBContext>();
            stubContext.Setup(x => x.Users).Returns(userDbSet);

            var sut = new UserRepository(stubContext.Object);
            var expectedUserWithEmail = "test2@gmail.com";
            //Act
            var result = sut.GetByEmail("test2@gmail.com");

            //Assert
            Assert.AreEqual(result.Email, expectedUserWithEmail);
        }

        [Test]
        public void Delete_PassInCorrectId_ShouldBeFail()
        {
            //Arrange            
            var collection = new List<User>
            {
                new User { Id = 1, Email = "test1@gmail.com", PasswordHash = "testUser1HashedPassword"},
                new User { Id = 2, Email = "test2@gmail.com", PasswordHash = "testUser2HashedPassword"},
                new User { Id = 3, Email = "test3@gmail.com", PasswordHash = "testUser3HashedPassword"}
            };

            var userDbSet = CreateDbSet(collection.AsQueryable());
            var stubContext = new Mock<IAppDBContext>();
            stubContext.Setup(x => x.Users).Returns(userDbSet);

            var sut = new UserRepository(stubContext.Object);

            //Act
            sut.Delete(4);
            var result = sut.GetAll().Count;
            //Assert
            Assert.IsFalse(result == 2);
        }
    }
}
