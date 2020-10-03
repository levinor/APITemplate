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

        public IEnumerable<UserDto> GetAllUsers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                List<UserDto> response =  context.Users
                        .Include(User =>User.Role)
                        .Include(User => User.Password)
                        .ToList();
                return response;
            }
        }

        public UserDto GetUserById(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                UserDto response = context.Users
                    .Include(User => User.Role)
                    .ToList()
                    .Where(u => u.UserId == id)
                    .FirstOrDefault();
                return response;
            }
        }

        public UserDto GetUserByEmail(string email)
        {
                IEnumerable<UserDto> response = GetAllUsers();
                return response.Where(u => u.Email == email).FirstOrDefault();
        }

        public void UpsertUser(UserDto user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                var role = context.Find<RoleDto>(user.Role.RoleId);
                user.Role = role;
                var modificator = context.Find<UserDto>(user.UserUpdated.UserId);
                user.UserUpdated = modificator;
                context.Update(user);
                context.SaveChanges();
            }
        }

        public void AddUser(UserDto user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                var role = context.Find<RoleDto>(user.Role.RoleId);
                user.Role = role;
                var modificator = context.Find<UserDto>(user.UserUpdated.UserId);
                user.UserUpdated = modificator;
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public RoleDto GetRoleById(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                RoleDto response = context.Roles                    
                    .ToList()
                    .Where(u => u.RoleId == id)
                    .FirstOrDefault();
                return response;
            }
        }
        public void DeleteUser(UserDto user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                user = context.Find<UserDto>(user.UserId);
                user.Active = false;               
                context.SaveChanges();
            }
        }


    }
}
