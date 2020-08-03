using Levinor.Business.EF.SQL;
using Levinor.Business.EF.SQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Levinor.Business.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(SQLEFContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                var roles = new Role[]
                {
                    new Role{Name="Administrator"},
                    new Role{Name="User"}
                };
                foreach (Role r in roles)
                {
                    context.Roles.Add(r);
                }
                context.SaveChanges();
            }
            if (!context.Passwords.Any())
            {
                var passwrod = new Password[]
               {
                    new Password{Pass="", ExpiringDate = DateTime.Now.AddMonths(1)},
                    new Password{Pass="", ExpiringDate = DateTime.Now.AddMonths(1)},
                    new Password{Pass="", ExpiringDate = DateTime.Now.AddMonths(1)},
                    new Password{Pass="", ExpiringDate = DateTime.Now.AddMonths(1)}
               };
                foreach (Password p in passwrod)
                {
                    context.Passwords.Add(p);
                }
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                        new User{Name="Carson", Surename="Alexander", Email="c.alexander@ecorp.com", RoleId = 1, DateUpdated = DateTime.Now, PasswordId=1 },
                        new User{Name="Alison", Surename="Smith", Email="a.smith@ecorp.com",RoleId = 1, DateUpdated = DateTime.Now, PasswordId=2 },
                        new User{Name="Emily", Surename="Snow", Email="e.snow@ecorp.com",RoleId = 2, DateUpdated = DateTime.Now, PasswordId=3 },
                        new User{Name="Jeff", Surename="Blacksmith", Email="j.blacksmith@ecorp.com",RoleId = 2, DateUpdated = DateTime.Now, PasswordId=4 }
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }           
        }
    }
}