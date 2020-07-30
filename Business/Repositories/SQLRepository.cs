﻿using Levinor.Business.EF.SQL;
using Levinor.Business.EF.SQL.Models;
using Levinor.Business.Repositories.Interfaces;
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
                List<User> response =  context.Users.ToList();
                return response;
            }
        }
    }
}