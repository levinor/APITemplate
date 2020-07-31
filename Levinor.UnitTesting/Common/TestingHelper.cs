using Levinor.Business.EF.SQL.Models;
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
                RoleId = 1
            });
            users.Add(new User
            {
                Id = 2,
                Name = "Bill",
                Surename = "Gates",
                Email = "bgates@ecorp.com",
                RoleId = 2
            });
            users.Add(new User
            {
                Id = 3,
                Name = "Alan",
                Surename = "Turing",
                Email = "aturing@ecorp.com",
                RoleId = 3
            });

            return users;
        }
    }
}
