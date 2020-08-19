using Levinor.Business.EF.SQL;
using Levinor.Business.EF.SQL.Models;
using Levinor.Business.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Levinor.Business.Repositories
{
    public class SQLRepository : ISQLRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public SQLRepository (IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;          
        }

        public IEnumerable<UserTable> GetAllUsers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                List<UserTable> response =  context.Users
                        .Include(User =>User.Role)
                        .Include(User => User.Password)
                        .ToList();
                return response;
            }
        }

        public UserTable GetUserById(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                UserTable response = context.Users
                    .Include(User => User.Role)
                    .ToList()
                    .Where(u => u.UserId == id)
                    .FirstOrDefault();
                return response;
            }
        }

        public UserTable GetUserByEmail(string email)
        {
                IEnumerable<UserTable> response = GetAllUsers();
                return response.Where(u => u.Email == email).FirstOrDefault();
        }

        public void UpsertUser(UserTable user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                var role = context.Find<RoleTable>(user.Role.RoleId);
                user.Role = role;
                var modificator = context.Find<UserTable>(user.UserUpdated.UserId);
                user.UserUpdated = modificator;
                context.Update(user);
                context.SaveChanges();
            }
        }

        public void AddUser(UserTable user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                var role = context.Find<RoleTable>(user.Role.RoleId);
                user.Role = role;
                var modificator = context.Find<UserTable>(user.UserUpdated.UserId);
                user.UserUpdated = modificator;
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public RoleTable GetRoleById(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                RoleTable response = context.Roles                    
                    .ToList()
                    .Where(u => u.RoleId == id)
                    .FirstOrDefault();
                return response;
            }
        }
        public void DeleteUser(string email)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                var user = GetUserByEmail(email);
                user = context.Find<UserTable>(user.UserId);
                context.Remove(user);
                context.SaveChanges();
            }
        }


    }
}
