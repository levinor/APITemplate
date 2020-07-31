using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services;
using Levinor.Business.Test.Common;
using Moq;
using NUnit.Framework;
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

        //This test is a little silly but is some sort of example
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
    }
}