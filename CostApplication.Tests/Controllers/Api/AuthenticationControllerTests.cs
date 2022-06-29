using CostApplication.Controllers.Api;
using CostApplication.Models;
using CostApplication.Models.Requests;
using CostApplication.Repositories;
using CostApplication.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Results;

namespace CostApplication.Tests.Controllers.Api
{
    [TestFixture]
    class AuthenticationControllerTests
    {      
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Login_PassLoginUser_ShouldBeOk()
        {
            //Arrange
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(
                     new User
                     {
                         Id = 1,
                         Email = "test@gmail.com",
                         AddedDate = DateTime.UtcNow
                     }
                );
            
            var pwdHasherMock = new Mock<IPasswordHashService>();
            pwdHasherMock
                .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            
            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            jwtTokenGeneratorMock
                .Setup(x => x.GenerateToken(It.IsAny<User>()))
                .Returns("Token Generated.");
            
            var sut = new AuthenticationApiController(userRepoMock.Object, pwdHasherMock.Object, jwtTokenGeneratorMock.Object);
            //Act
            var result = sut.Login(new LoginRequest());


            //Assert
            Assert.That(result, Is.Not.Null);            

            userRepoMock.Verify(mock => mock.GetByEmail(It.IsAny<string>()), Times.Once());   
            pwdHasherMock.Verify(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once()); 
            jwtTokenGeneratorMock.Verify(mock => mock.GenerateToken(It.IsAny<User>()), Times.Once());            
        }

        [Test]
        public void Login_LoginRequestModelValidation_ShouldBeOk()
        {
            //Arrange
            var sut = new LoginRequest
            { 
                Email = "test@test.com",
                Password = "testPassword"
            };

            //Act
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            //Assert
            Assert.IsTrue(isModelStateValid);
        }

        [Test]
        public void Login_LoginRequestModelValidation_ShouldBeFalse()
        {
            //Arrange
            var sut = new LoginRequest();
         

            //Act
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            //Assert
            Assert.IsFalse(isModelStateValid);
        }

        [Test]
        public void Login_PassIncorectModel_ShouldBeFalse()
        {
            //Arrange
            var userRepoMock = new Mock<IUserRepository>();
            var pwdHasherMock = new Mock<IPasswordHashService>();
            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            var sut = new AuthenticationApiController(userRepoMock.Object, pwdHasherMock.Object, jwtTokenGeneratorMock.Object);
            sut.ModelState.AddModelError("test","test");
            //Act
            _ = sut.Login(new LoginRequest());
            var modelState = sut.ModelState;

            //Assert
            Assert.IsFalse(modelState.IsValid);
        }

        [Test]
        public void Login_PassCorectModel_ShouldBeOk()
        {
            //Arrange
            var userRepoMock = new Mock<IUserRepository>();
            var pwdHasherMock = new Mock<IPasswordHashService>();
            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            var sut = new AuthenticationApiController(userRepoMock.Object, pwdHasherMock.Object, jwtTokenGeneratorMock.Object);
            sut.ModelState.Clear();
            //Act
            _ = sut.Login(new LoginRequest() { Email = "test@test.com", Password = "test"});
            var modelState = sut.ModelState;

            //Assert
            Assert.IsTrue(modelState.IsValid);
        }
    }
}
