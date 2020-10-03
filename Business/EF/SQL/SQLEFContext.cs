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

        public DbSet<UserDto> Users { get; set; }
        public DbSet<PasswordDto> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserDto
            modelBuilder.Entity<UserDto>()
                .Property(p => p.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserDto>()
               .HasKey(k => k.UserId);

            modelBuilder.Entity<UserDto>()
                 .HasOne(u => u.Updater)
                 .WithMany()
                 .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<UserDto>()
                 .HasOne(u => u.Supervisor)
                 .WithMany()
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDto>()
                 .HasOne(p => p.Password)
                 .WithOne(u => u.User)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region PasswordDto
            modelBuilder.Entity<PasswordDto>()
                .Property(p => p.PasswordId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PasswordDto>()
               .HasKey(k => k.PasswordId);
            #endregion

        }
    }   
}
