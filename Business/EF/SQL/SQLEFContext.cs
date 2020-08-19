using Levinor.Business.Domain;
using Levinor.Business.EF.SQL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace Levinor.Business.EF.SQL
{
    public class SQLEFContext : DbContext
    {
        public SQLEFContext(DbContextOptions<SQLEFContext> dbContextOptions): base(dbContextOptions)
        {
            Database.Migrate();
        }

        public DbSet<UserTable> Users { get; set; }
        public DbSet<RoleTable> Roles { get; set; }
        public DbSet<PasswordTable> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserTable
            modelBuilder.Entity<UserTable>()
                .Property(p => p.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserTable>()
               .HasKey(k => k.UserId);

            modelBuilder.Entity<UserTable>()
                 .HasOne(u => u.UserUpdated)
                 .WithMany()
                 .OnDelete(DeleteBehavior.NoAction);
           
            modelBuilder.Entity<UserTable>()
                 .HasOne(r => r.Role)
                 .WithMany(u => u.Users)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserTable>()
                 .HasOne(p => p.Password)
                 .WithOne(u => u.User)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region PasswordTable
            modelBuilder.Entity<PasswordTable>()
                .Property(p => p.PasswordId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PasswordTable>()
               .HasKey(k => k.PasswordId);
            #endregion

            #region RoleTable
            modelBuilder.Entity<RoleTable>()
                .Property(p => p.RoleId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RoleTable>()
               .HasKey(k => k.RoleId);
            #endregion
        }
    }   
}
