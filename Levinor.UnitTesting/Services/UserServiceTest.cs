using AutoMapper;
using Levinor.Business.Domain;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services;
using Levinor.Business.Services.Interfaces;
using Levinor.Business.Test.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.UserTable>()))
                .Returns((EF.SQL.Models.UserTable source) =>
                {
                    Domain.User returnUser = new Domain.User();
                    returnUser.UserId = source.UserId;
                    returnUser.Name = source.Name;
                    //returnUser.Role = source.Role;
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

            this.mockedRepository.Setup(x => x.GetUserById(value)).Returns(mockedResponse.Where(u => u.UserId == value).FirstOrDefault());
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.UserTable>()))
               .Returns((EF.SQL.Models.UserTable source) =>
               {
                   Domain.User returnUser = new Domain.User();
                   returnUser.UserId = source.UserId;
                   returnUser.Name = source.Name;
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

            this.mockedRepository.Setup(x => x.GetUserById(value)).Returns(mockedResponse.Where(u => u.UserId == value).FirstOrDefault());
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.UserTable>()))
              .Returns((EF.SQL.Models.UserTable source) =>
              {
                  Domain.User returnUser = new Domain.User();
                  returnUser.UserId = source.UserId;
                  returnUser.Name = source.Name;
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
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.UserTable>()))
              .Returns((EF.SQL.Models.UserTable source) =>
              {
                  Domain.User returnUser = new Domain.User();
                  returnUser.UserId = source.UserId;
                  returnUser.Name = source.Name;
                  returnUser.Surename = source.Surename;
                  returnUser.Password = new Password
                  {
                      PasswordId = source.Password.PasswordId,
                      CurrentPassword = source.Password.Password,
                      ExpiringDate = source.Password.ExpiringDate
                  };
                  return returnUser;
              });
            this.mockedCache.Setup(x => x.generateUserToken(new Domain.User())).Returns(Guid.NewGuid());

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            User UserRequest = new User
            {
                Email = email
            };
            Password PasswordRequest = new Password
            {
                CurrentPassword = pass
            };

            //Act
            var token = service.GetLoginToken(UserRequest, PasswordRequest);

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
            this.mockedMapper.Setup(x => x.Map<Domain.User>(It.IsAny<EF.SQL.Models.UserTable>()))
              .Returns((EF.SQL.Models.UserTable source) =>
              {
                  Domain.User returnUser = new Domain.User();
                  returnUser.UserId = source.UserId;
                  returnUser.Name = source.Name;
                  returnUser.Surename = source.Surename;
                  returnUser.Password = new Password
                  {
                      PasswordId = source.Password.PasswordId,
                      CurrentPassword = source.Password.Password,
                      ExpiringDate = source.Password.ExpiringDate
                  };
                  return returnUser;
              });
            this.mockedCache.Setup(x => x.generateUserToken(new Domain.User())).Returns(Guid.NewGuid());

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            User UserRequest = new User
            {
                Email = email
            };
            Password PasswordRequest = new Password
            {
                CurrentPassword = pass
            };

            //Act
            var ex = Assert.Throws<ArgumentException>(() => service.GetLoginToken(UserRequest, PasswordRequest));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.GetLoginToken(UserRequest, PasswordRequest)).Message.Contains("Incorrect Password or User"));
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "ltorvalds@ecorp.com", "string", "p4ssw0rd")]
        public void SetNewPassword_Success(string token, string email, string currentPass, string newPass)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            User user = new User
            {
                UserId = mockedResponse[0].UserId,
                Name = mockedResponse[0].Name,
                Surename = mockedResponse[0].Surename,
                Email = mockedResponse[0].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[0].Password.PasswordId,
                    CurrentPassword = mockedResponse[0].Password.Password,
                    ExpiringDate = mockedResponse[0].Password.ExpiringDate
                }
            };

            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out  user)).Returns(true);
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.UserTable>(It.IsAny<Domain.User>()))
                .Returns((Domain.User source) =>
                {
                    EF.SQL.Models.UserTable returnUser = new EF.SQL.Models.UserTable();
                    returnUser.UserId = source.UserId;
                    returnUser.Name = source.Name;
                    returnUser.Surename = source.Surename;
                    returnUser.Password = new EF.SQL.Models.PasswordTable
                    {
                        PasswordId = source.Password.PasswordId,
                        Password = source.Password.CurrentPassword,
                        ExpiringDate = source.Password.ExpiringDate
                    };
                    return returnUser;
                });

            this.mockedRepository.Setup(x => x.UpsertUser(It.IsAny<EF.SQL.Models.UserTable>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);


            User UserRequest = new User
            {
                Email = email
            };
            Password PasswordRequest = new Password
            {
                CurrentPassword = currentPass,
                NewPassword = newPass
            };

            service.SetNewPassword(Guid.Parse(token), UserRequest, PasswordRequest);

            // If execution don't throw an exception the method went ok.
            Assert.True(true);
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "incorrecto@ecorp.com", "string", "p4ssw0rd")]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "ltorvalds@ecorp.com", "incorrect", "p4ssw0rd")]
        public void SetNewPassword_Fail(string token, string email, string currentPass, string newPass)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            User user = new User
            {
                UserId = mockedResponse[0].UserId,
                Name = mockedResponse[0].Name,
                Surename = mockedResponse[0].Surename,
                Email = mockedResponse[0].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[0].Password.PasswordId,
                    CurrentPassword = mockedResponse[0].Password.Password,
                    ExpiringDate = mockedResponse[0].Password.ExpiringDate
                }
            };

            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out user)).Returns(true);
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.UserTable>(It.IsAny<Domain.User>()))
                .Returns((Domain.User source) =>
                {
                    EF.SQL.Models.UserTable returnUser = new EF.SQL.Models.UserTable();
                    returnUser.UserId = source.UserId;
                    returnUser.Name = source.Name;
                    returnUser.Surename = source.Surename;
                    returnUser.Password = new EF.SQL.Models.PasswordTable
                    {
                        PasswordId = source.Password.PasswordId,
                        Password = source.Password.CurrentPassword,
                        ExpiringDate = source.Password.ExpiringDate
                    };
                    return returnUser;
                });

            this.mockedRepository.Setup(x => x.UpsertUser(It.IsAny<EF.SQL.Models.UserTable>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            User UserRequest = new User
            {
                Email = email
            };
            Password PasswordRequest = new Password
            {
                CurrentPassword = currentPass,
                NewPassword = newPass
            };

            var ex = Assert.Throws<ArgumentException>(() => service.SetNewPassword(Guid.Parse(token), UserRequest, PasswordRequest));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.SetNewPassword(Guid.Parse(token), UserRequest, PasswordRequest)).Message.Contains("password"));
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "p4ssw0rd")]
        public void SetNewUser_Success(string token, string password)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            User Creator = new User
            {
                UserId = mockedResponse[0].UserId,
                Name = mockedResponse[0].Name,
                Surename = mockedResponse[0].Surename,
                Email = mockedResponse[0].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[0].Password.PasswordId,
                    CurrentPassword = mockedResponse[0].Password.Password,
                    ExpiringDate = mockedResponse[0].Password.ExpiringDate
                },
                Role = new Role
                {
                    RoleId = mockedResponse[0].Role.RoleId,
                    Name = mockedResponse[0].Role.Name
                }
            };
            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out Creator)).Returns(true);

            
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.UserTable>(It.IsAny<Domain.User>()))
                .Returns((Domain.User source) =>
                {
                    EF.SQL.Models.UserTable returnUser = new EF.SQL.Models.UserTable();
                    returnUser.UserId = source.UserId;
                    returnUser.Name = source.Name;
                    returnUser.Surename = source.Surename;
                    returnUser.Password = new EF.SQL.Models.PasswordTable
                    {
                        PasswordId = source.Password.PasswordId,
                        Password = source.Password.CurrentPassword,
                        ExpiringDate = source.Password.ExpiringDate
                    };
                    return returnUser;
                });
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.RoleTable>(It.IsAny<Domain.Role>()))
               .Returns((Domain.Role source) =>
               {
                   EF.SQL.Models.RoleTable returnRole = new EF.SQL.Models.RoleTable();
                   returnRole.RoleId = source.RoleId;
                   returnRole.Name = source.Name;   
                   return returnRole;
               });

            this.mockedRepository.Setup(x => x.AddUser(It.IsAny<EF.SQL.Models.UserTable>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);
           
            Password passwordModel = new Password
            {
                PasswordId = 666,
                NewPassword = password,
                ExpiringDate = DateTime.Now.AddDays(30)
            };

            Role roleModel = new Role
            {
                RoleId = 1,
                Name = "Administrator"
            };

            User newUSer = new User
            {
                UserId = 666,
                Name = "L",
                Surename = "A",
                Email = "e@ecorp.com",
                Password = passwordModel,
                Role = roleModel
            };

            service.SetNewUser(Guid.Parse(token), newUSer, passwordModel, roleModel);

            // If execution don't throw an exception the method went ok.
            Assert.True(true);

        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", "p4ssw0rd", 2)]
        public void SetNewUser_Fail(string token, string password, int creatorIndex)
        {
            //Assert
            var mockedResponse = _helper.getUsersList();

            User Creator = new User
            {
                UserId = mockedResponse[creatorIndex].UserId,
                Name = mockedResponse[creatorIndex].Name,
                Surename = mockedResponse[creatorIndex].Surename,
                Email = mockedResponse[creatorIndex].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[creatorIndex].Password.PasswordId,
                    CurrentPassword = mockedResponse[creatorIndex].Password.Password,
                    ExpiringDate = mockedResponse[creatorIndex].Password.ExpiringDate
                },
                Role = new Role
                {
                    RoleId = mockedResponse[creatorIndex].Role.RoleId,
                    Name = mockedResponse[creatorIndex].Role.Name
                }
            };
            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out Creator)).Returns(true);


            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.UserTable>(It.IsAny<Domain.User>()))
                .Returns((Domain.User source) =>
                {
                    EF.SQL.Models.UserTable returnUser = new EF.SQL.Models.UserTable();
                    returnUser.UserId = source.UserId;
                    returnUser.Name = source.Name;
                    returnUser.Surename = source.Surename;
                    returnUser.Password = new EF.SQL.Models.PasswordTable
                    {
                        PasswordId = source.Password.PasswordId,
                        Password = source.Password.CurrentPassword,
                        ExpiringDate = source.Password.ExpiringDate
                    };
                    return returnUser;
                });
            this.mockedMapper.Setup(x => x.Map<EF.SQL.Models.RoleTable>(It.IsAny<Domain.Role>()))
               .Returns((Domain.Role source) =>
               {
                   EF.SQL.Models.RoleTable returnRole = new EF.SQL.Models.RoleTable();
                   returnRole.RoleId = source.RoleId;
                   returnRole.Name = source.Name;
                   return returnRole;
               });

            this.mockedRepository.Setup(x => x.AddUser(It.IsAny<EF.SQL.Models.UserTable>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            Password passwordModel = new Password
            {
                PasswordId = 666,
                NewPassword = password,
                ExpiringDate = DateTime.Now.AddDays(30)
            };

            Role roleModel = new Role
            {
                RoleId = 1,
                Name = "Administrator"
            };

            User newUSer = new User
            {
                UserId = 666,
                Name = "L",
                Surename = "A",
                Email = "e@ecorp.com",
                Password = passwordModel,
                Role = roleModel
            };


            var ex = Assert.Throws<ArgumentException>(() => service.SetNewUser(Guid.Parse(token), newUSer, passwordModel, roleModel));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.SetNewUser(Guid.Parse(token), newUSer, passwordModel, roleModel)).GetType().Name == "ArgumentException");
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662")]
        public void DeleteUser_Success(string token)
        {

            //Assert
            var mockedResponse = _helper.getUsersList();

            User Deleter = new User
            {
                UserId = mockedResponse[0].UserId,
                Name = mockedResponse[0].Name,
                Surename = mockedResponse[0].Surename,
                Email = mockedResponse[0].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[0].Password.PasswordId,
                    CurrentPassword = mockedResponse[0].Password.Password,
                    ExpiringDate = mockedResponse[0].Password.ExpiringDate
                },
                Role = new Role
                {
                    RoleId = mockedResponse[0].Role.RoleId,
                    Name = mockedResponse[0].Role.Name
                }
            };
            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out Deleter)).Returns(true);
            this.mockedRepository.Setup(x => x.DeleteUser(It.IsAny<EF.SQL.Models.UserTable>()));
            this.mockedRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new EF.SQL.Models.UserTable());

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);
            service.DeleteUser(Guid.Parse(token), "ll@ecorp.com");

            // If execution don't throw an exception the method went ok.
            Assert.True(true);
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", 2)]
        public void DeleteUser_FailNotAdmin(string token, int creatorIndex)
        {

            //Assert
            var mockedResponse = _helper.getUsersList();


            User Deleter = new User
            {
                UserId = mockedResponse[creatorIndex].UserId,
                Name = mockedResponse[creatorIndex].Name,
                Surename = mockedResponse[creatorIndex].Surename,
                Email = mockedResponse[creatorIndex].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[creatorIndex].Password.PasswordId,
                    CurrentPassword = mockedResponse[creatorIndex].Password.Password,
                    ExpiringDate = mockedResponse[creatorIndex].Password.ExpiringDate
                },
                Role = new Role
                {
                    RoleId = mockedResponse[creatorIndex].Role.RoleId,
                    Name = mockedResponse[creatorIndex].Role.Name
                }
            };
            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out Deleter)).Returns(true);
            
            this.mockedRepository.Setup(x => x.DeleteUser(It.IsAny<EF.SQL.Models.UserTable>()));
            this.mockedRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new EF.SQL.Models.UserTable());

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            // If execution don't throw an exception the method went ok.
            var ex = Assert.Throws<ArgumentException>(() => service.DeleteUser(Guid.Parse(token), "ll@ecorp.com"));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.DeleteUser(Guid.Parse(token), "ll@ecorp.com")).GetType().Name == "ArgumentException");
        }

        [Test]
        [TestCase("29ffd8bf-8f32-4b1b-9d66-93f842802662", 1)]
        public void DeleteUser_FailNotUSer(string token, int creatorIndex)
        {

            //Assert
            var mockedResponse = _helper.getUsersList();


            User Deleter = new User
            {
                UserId = mockedResponse[creatorIndex].UserId,
                Name = mockedResponse[creatorIndex].Name,
                Surename = mockedResponse[creatorIndex].Surename,
                Email = mockedResponse[creatorIndex].Email,
                Password = new Password
                {
                    PasswordId = mockedResponse[creatorIndex].Password.PasswordId,
                    CurrentPassword = mockedResponse[creatorIndex].Password.Password,
                    ExpiringDate = mockedResponse[creatorIndex].Password.ExpiringDate
                },
                Role = new Role
                {
                    RoleId = mockedResponse[creatorIndex].Role.RoleId,
                    Name = mockedResponse[creatorIndex].Role.Name
                }
            };
            this.mockedCache.Setup(x => x.checkAuthToken(Guid.Parse(token), out Deleter)).Returns(true);

            this.mockedRepository.Setup(x => x.DeleteUser(It.IsAny<EF.SQL.Models.UserTable>()));
            this.mockedRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>()));

            service = new UserService(mockedRepository.Object, mockedMapper.Object, mockedCache.Object);

            // If execution don't throw an exception the method went ok.
            var ex = Assert.Throws<ArgumentException>(() => service.DeleteUser(Guid.Parse(token), "ll@ecorp.com"));

            //Assert
            Assert.That(Assert.Throws<ArgumentException>(() => service.DeleteUser(Guid.Parse(token), "ll@ecorp.com")).GetType().Name == "ArgumentException");
        }
    }
}