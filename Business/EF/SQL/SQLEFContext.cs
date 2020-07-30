using Levinor.Business.EF.SQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Levinor.Business.EF.SQL
{
    public class SQLEFContext : DbContext
    {
        public SQLEFContext(DbContextOptions<SQLEFContext> dbContextOptions): base(dbContextOptions)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
        }
    }   
}
