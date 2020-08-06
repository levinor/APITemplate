using Levinor.Business.EF.SQL;
using Levinor.Business.EF.SQL.Models;
using Levinor.Business.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.Business.Repositories
{
    public class SQLRepository : ISQLRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public SQLRepository (IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;          
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                List<User> response =  context.Users
                        .Include(User =>User.Role)
                        .ToList();
                return response;
            }
        }

        public User GetUserById(int Id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                User response = context.Users
                    .Include(User => User.Role)
                    .ToList()
                    .Where(u => u.Id == Id)
                    .FirstOrDefault();
                return response;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                User response = context.Users
                    .Include(User => User.Role)
                    .Include(User => User.Password)
                    .ToList()
                    .Where(u => u.Email == email)
                    .FirstOrDefault();
                return response;
            }
        }

        public void UpdateUser(User user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SQLEFContext>();
                var entry = context.Entry(user);
                entry.State = EntityState.Modified;
                var entryPass = context.Entry(user.Password);
                entryPass.State = EntityState.Modified;
                var entryRole = context.Entry(user.Role);
                entryRole.State = EntityState.Modified;
                context.SaveChanges();
            }
        }


    }
}
