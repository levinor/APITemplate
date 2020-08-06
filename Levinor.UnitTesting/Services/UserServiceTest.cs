using AutoMapper;
using Levinor.Business.Domain;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services;
using Levinor.Business.Services.Interfaces;
using Levinor.Business.Test.Common;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace Levinor.Business.Test
{
    public class UserServiceTest
    {
        private readonly Mock<ISQLRepository> mockedRepository;
        private readonly TestingHelper _helper;
        private readonly Mock<IMapper> mockedMapper;
        private UserService service;
        private readonly Mock<ICacheService> mockedCache;
  
        public UserServiceTest()
        {
            mockedRepository = new Mock<ISQLRepository>();
            mockedMapper = new Mock<IMapper>();
            mockedCache = new Mock<ICacheService>();
            _helper = new TestingHelper();

        }

        ///This test is a little silly but is some sort of example
        [Test]
        public void GetAllUsers_Sucess()
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            this.mockedRepository.Setup(x => x.GetAllUsers()).Returns(mockedResponse);
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.User>()))
                .Returns((EF.SQL.Models.User source) =>
                {
                    Domain.User returnUser = new Domain.User();
                    returnUser.Id = source.Id;
                    returnUser.Name = source.Name;
                    returnUser.Role = source.Role;
                    returnUser.Surename = source.Surename;
                    return returnUser;
                });

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

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
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.User>()))
               .Returns((EF.SQL.Models.User source) =>
               {
                   Domain.User returnUser = new Domain.User();
                   returnUser.Id = source.Id;
                   returnUser.Name = source.Name;
                   returnUser.Role = source.Role;
                   returnUser.Surename = source.Surename;
                   return returnUser;
               });

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

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
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.User>()))
              .Returns((EF.SQL.Models.User source) =>
              {
                  Domain.User returnUser = new Domain.User();
                  returnUser.Id = source.Id;
                  returnUser.Name = source.Name;
                  returnUser.Role = source.Role;
                  returnUser.Surename = source.Surename;
                  return returnUser;
              });

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => service.GetUserById(value));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.GetUserById(value)).Message.StartsWith("User with ID:"));
        }

        [Test]
        [TestCase("ltorvalds@ecorp.com", "string")]
        [TestCase("bgates@ecorp.com", "string")]
        [TestCase("aturing@ecorp.com", "string")]
        public void GetLoginToken_Success(string email, string pass)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            this.mockedRepository.Setup(x => x.GetUserByEmail(email)).Returns(mockedResponse.Where(u => u.Email == email).FirstOrDefault());
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.User>()))
              .Returns((EF.SQL.Models.User source) =>
              {
                  Domain.User returnUser = new Domain.User();
                  returnUser.Id = source.Id;
                  returnUser.Name = source.Name;
                  returnUser.Role = source.Role;
                  returnUser.Surename = source.Surename;
                  returnUser.Password = source.Password;
                  return returnUser;
              });
            this.mockedCache.Setup(x => x.generateUserToken(new Domain.User())).Returns(Guid.NewGuid());

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            //Act
            var token = service.GetLoginToken(email, pass);

            //Assert
            Assert.True(token.token.ToString().Length > 0 && !token.needsToBeUpdated);
        }

        [Test]
        [TestCase("incorrect@ecorp.com", "string")]
        [TestCase("bgates@ecorp.com", "incorrect")]
        public void GetLoginToken_Fail(string email, string pass)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            this.mockedRepository.Setup(x => x.GetUserByEmail(email)).Returns(mockedResponse.Where(u => u.Email == email).FirstOrDefault());
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.User>()))
              .Returns((EF.SQL.Models.User source) =>
              {
                  Domain.User returnUser = new Domain.User();
                  returnUser.Id = source.Id;
                  returnUser.Name = source.Name;
                  returnUser.Role = source.Role;
                  returnUser.Surename = source.Surename;
                  returnUser.Password = source.Password;
                  return returnUser;
              });
            this.mockedCache.Setup(x => x.generateUserToken(new Domain.User())).Returns(Guid.NewGuid());

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => service.GetLoginToken(email, pass));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.GetLoginToken(email, pass)).Message.Contains("Incorrect Password or User"));
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "ltorvalds@ecorp.com", "string", "p4ssw0rd")]
        public void SetNewPassword_Success(string token, string email, string currentPass, string Newpass)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            User user = new User
            {
                Id = mockedResponse[0].Id,
                Name = mockedResponse[0].Name,
                Surename = mockedResponse[0].Surename,
                Email = mockedResponse[0].Email,
                Password = mockedResponse[0].Password
            };

            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out  user)).Returns(true);
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.User>(It.IsAny<Domain.User>()))
                .Returns((Domain.User source) =>
                {
                    EF.SQL.Models.User returnUser = new EF.SQL.Models.User();
                    returnUser.Id = source.Id;
                    returnUser.Name = source.Name;
                    returnUser.Role = source.Role;
                    returnUser.Surename = source.Surename;
                    returnUser.Password = source.Password;
                    return returnUser;
                });

            this.mockedRepository.Setup(x => x.UpdateUser(It.IsAny<EF.SQL.Models.User>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            service.SetNewPassword(token, email, currentPass, Newpass);

            // If execution don't throw an exception the method went ok.
            Assert.True(true);
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "incorrecto@ecorp.com", "string", "p4ssw0rd")]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "ltorvalds@ecorp.com", "incorrect", "p4ssw0rd")]
        public void SetNewPassword_Fail(string token, string email, string currentPass, string Newpass)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            User user = new User
            {
                Id = mockedResponse[0].Id,
                Name = mockedResponse[0].Name,
                Surename = mockedResponse[0].Surename,
                Email = mockedResponse[0].Email,
                Password = mockedResponse[0].Password
            };

            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out user)).Returns(true);
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.User>(It.IsAny<Domain.User>()))
                .Returns((Domain.User source) =>
                {
                    EF.SQL.Models.User returnUser = new EF.SQL.Models.User();
                    returnUser.Id = source.Id;
                    returnUser.Name = source.Name;
                    returnUser.Role = source.Role;
                    returnUser.Surename = source.Surename;
                    returnUser.Password = source.Password;
                    return returnUser;
                });

            this.mockedRepository.Setup(x => x.UpdateUser(It.IsAny<EF.SQL.Models.User>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            var ex = Assert.Throws<ArgumentException>(() => service.SetNewPassword(token, email, currentPass, Newpass));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.SetNewPassword(token, email, currentPass, Newpass)).Message.Contains("password"));
        }



    }
}