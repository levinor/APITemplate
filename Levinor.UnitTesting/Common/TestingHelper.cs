using Levinor.Business.EF.SQL.Models;
using System;
using System.Collections.Generic;

namespace Levinor.Business.Test.Common
{
    public class TestingHelper
    {
        public List<User> getUsersList()
        {
            List<User> users = new List<User>();
            users.Add(new User {
                Id = 1,
                Name = "Linus",
                Surename = "Torvalds",
                Email ="ltorvalds@ecorp.com",
                RoleId = 1,
                Password = new Password { Id = 1, Pass = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6)}
            });
            users.Add(new User
            {
                Id = 2,
                Name = "Bill",
                Surename = "Gates",
                Email = "bgates@ecorp.com",
                RoleId = 2,
                Password = new Password { Id = 2, Pass = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6) }

            });
            users.Add(new User
            {
                Id = 3,
                Name = "Alan",
                Surename = "Turing",
                Email = "aturing@ecorp.com",
                RoleId = 3,
                Password = new Password { Id = 3, Pass = "1Wq7z1ZG3W7ybdhXwFFktGTBLB/oLItx+86MWN6cSW4=", ExpiringDate = DateTime.Now.AddMonths(6) }
            });

            return users;
        }
    }
}
