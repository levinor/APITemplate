using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services;
using Levinor.Business.Test.Common;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Linq;

namespace Levinor.Business.Test
{
    public class UserServiceTest
    {
        private readonly Mock<ISQLRepository> mockedRepository;
        private readonly TestingHelper _helper;
        private UserService service;
  
        public UserServiceTest()
        {
            mockedRepository = new Mock<ISQLRepository>();
            _helper = new TestingHelper();
        }

        ///This test is a little silly but is some sort of example
        [Test]
        public void GetAllUsers_Sucess()
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            this.mockedRepository.Setup(x => x.GetAllUsers()).Returns(mockedResponse);

            service = new UserService(mockedRepository.Object);

            //Act
            var response = service.GetAllUsers();

            //Assert
            Assert.True(response.ToList().Count == 3);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetUserById_Success(int value)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            this.mockedRepository.Setup(x => x.GetUserById(value)).Returns(mockedResponse.Where(u => u.Id == value).FirstOrDefault());

            service = new UserService(mockedRepository.Object);

            //Act
            var response = service.GetUserById(value);

            //Assert
            Assert.True(response != null);
        }

        [Test]
        [TestCase(4)]
        public void GetUserById_Fail(int value)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            this.mockedRepository.Setup(x => x.GetUserById(value)).Returns(mockedResponse.Where(u => u.Id == value).FirstOrDefault());

            service = new UserService(mockedRepository.Object);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => service.GetUserById(value));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.GetUserById(value)).Message.StartsWith("User with ID:"));
        }
    }
}