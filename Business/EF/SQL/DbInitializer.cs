using Levinor.Business.EF.SQL;
using Levinor.Business.EF.SQL.Models;
using System.Linq;

namespace Levinor.Business.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(SQLEFContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
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

            var users = new User[]
            {
                new User{Name="Carson", Surename="Alexander", Email="c.alexander@ecorp.com",RoleId = 1 },
                new User{Name="Alison", Surename="Smith", Email="a.smith@ecorp.com",RoleId = 1 },
                new User{Name="Emily", Surename="Snow", Email="e.snow@ecorp.com",RoleId = 2 },
                new User{Name="Jeff", Surename="Blacksmith", Email="j.blacksmith@ecorp.com",RoleId = 2 }
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

           
        }
    }
}