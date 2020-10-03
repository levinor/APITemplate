using Levinor.Business.EF.SQL.Models;
using System;
using System.Collections.Generic;

namespace Levinor.Business.Test.Common
{
    public class TestingHelper
    {
        public List<UserTable> getUsersList()
        {
            List<UserTable> users = new List<UserTable>();
            users.Add(new UserTable {
                UserId = 1,
                Name = "Linus",
                Surename = "Torvalds",
                Email ="ltorvalds@ecorp.com",
                Role = new RoleTable { RoleId = 1, Name = "Administrator"},
                Password = new PasswordTable { PasswordId = 1, Password = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6)}
            });
            users.Add(new UserTable
            {
                UserId = 2,
                Name = "Bill",
                Surename = "Gates",
                Email = "bgates@ecorp.com",
                Role = new RoleTable { RoleId = 1, Name = "Administrator" },
                Password = new PasswordTable { PasswordId = 2, Password = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6) }

            });
            users.Add(new UserTable
            {
                UserId = 3,
                Name = "Alan",
                Surename = "Turing",
                Email = "aturing@ecorp.com",
                Role = new RoleTable { RoleId = 2, Name = "User" },
                Password = new PasswordTable { PasswordId = 3, Password = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6) }
            });

            return users;
        }
    }
}
