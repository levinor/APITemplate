using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Levinor.Business.EF.SQL
{
    public class SQLEFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurename { get; set; }
        public string UserEmail { get; set; }
        public int UserRoleId { get; set; }
        public virtual Role Role { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
