using Levinor.Business.EF.SQL.Models;
using System;
using System.Collections.Generic;

namespace Levinor.Business.Test.Common
{
    public class TestingHelper
    {
        public List<UserDto> getUsersList()
        {
            List<UserDto> users = new List<UserDto>();
            users.Add(new UserDto {
                UserId = 1,
                Name = "Linus",
                Surename = "Torvalds",
                Email ="ltorvalds@ecorp.com",
                Role = new RoleDto { RoleId = 1, Name = "Administrator"},
                Password = new PasswordDto { PasswordId = 1, Password = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6)}
            });
            users.Add(new UserDto
            {
                UserId = 2,
                Name = "Bill",
                Surename = "Gates",
                Email = "bgates@ecorp.com",
                Role = new RoleDto { RoleId = 1, Name = "Administrator" },
                Password = new PasswordDto { PasswordId = 2, Password = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6) }

            });
            users.Add(new UserDto
            {
                UserId = 3,
                Name = "Alan",
                Surename = "Turing",
                Email = "aturing@ecorp.com",
                Role = new RoleDto { RoleId = 2, Name = "User" },
                Password = new PasswordDto { PasswordId = 3, Password = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6) }
            });

            return users;
        }
    }
}
