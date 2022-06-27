using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace TestCostApplication.UnitTests.Controller.Api
{
    [TestClass]
    public class AuthenticationApiControllerTests
    {
        [Test]
        public void TestMethod1()
        {
            //Arrange
            Mock<IPasswordHashService> mockPassHashService = new Mock<IPasswordHashService>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            //Act

            //Assert
        }
    }
}
