using Levinor.Business.EF.SQL;
using Levinor.Business.EF.SQL.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
                var roles = new RoleDto[]
                {
                    new RoleDto{Name="Administrator"},
                    new RoleDto{Name="User"}
                };
                foreach (RoleDto r in roles)
                {
                    context.Roles.Add(r);
                }

                var passwrod = new PasswordDto[]
                   {
                        new PasswordDto{Password="pg3G0EkXXiMMdg1EojpjQZXrki5lU7/s7lPhTBEtBVo=", ExpiringDate = DateTime.Now.AddMonths(1)},
                        new PasswordDto{Password="pg3G0EkXXiMMdg1EojpjQZXrki5lU7/s7lPhTBEtBVo=", ExpiringDate = DateTime.Now.AddMonths(1)},
                        new PasswordDto{Password="pg3G0EkXXiMMdg1EojpjQZXrki5lU7/s7lPhTBEtBVo=", ExpiringDate = DateTime.Now.AddMonths(1)},
                        new PasswordDto{Password="pg3G0EkXXiMMdg1EojpjQZXrki5lU7/s7lPhTBEtBVo=", ExpiringDate = DateTime.Now.AddMonths(1)}
                   };
                foreach (PasswordDto p in passwrod)
                {
                    context.Passwords.Add(p);
                }
          
                var users = new UserDto[]
                {
                        new UserDto{Name="Carson", Surename="Alexander", Email="c.alexander@ecorp.com", Role = roles[0], DateUpdated = DateTime.Now, Password = passwrod[0] },
                        new UserDto{Name="Alison", Surename="Smith", Email="a.smith@ecorp.com",Role = roles[0], DateUpdated = DateTime.Now, Password = passwrod[1] },
                        new UserDto{Name="Emily", Surename="Snow", Email="e.snow@ecorp.com",Role = roles[1], DateUpdated = DateTime.Now, Password = passwrod[2] },
                        new UserDto{Name="Jeff", Surename="Blacksmith", Email="j.blacksmith@ecorp.com",Role = roles[1], DateUpdated = DateTime.Now, Password = passwrod[3] }
                };
                foreach (UserDto u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }           
        }
    }
}