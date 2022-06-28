using CostApplication.Controllers.Api;
using CostApplication.Models;
using CostApplication.Models.Requests;
using CostApplication.Repositories;
using CostApplication.Services;
using Moq;
using NUnit.Framework;
using System;

namespace CostApplication.Tests.Controllers.Api
{
    [TestFixture]
    class AuthenticationControllerTests
    {
        private AuthenticationApiController _controller;

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestLogin()
        {
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
            _controller = new AuthenticationApiController(userRepoMock.Object, pwdHasherMock.Object, jwtTokenGeneratorMock.Object);

            var result = _controller.Login(new LoginRequest());

            Assert.That(result, Is.Not.Null);
            Assert.DoesNotThrow(() =>
            {
                userRepoMock.Verify(mock => mock.GetByEmail(It.IsAny<string>()), Times.Once());
            });
            Assert.DoesNotThrow(() =>
            {
                pwdHasherMock.Verify(mock => mock.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            });
            Assert.DoesNotThrow(() =>
            {
                jwtTokenGeneratorMock.Verify(mock => mock.GenerateToken(It.IsAny<User>()), Times.Once());
            });
        }
    }
}
