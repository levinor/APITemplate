using Levinor.Business.EF.SQL.Models;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Levinor.Business.Services
{
    public class UserService : IUserService
    {
        private ISQLRepository _repository;
        public UserService(ISQLRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<User> GetAllUsers()
        {           
            return _repository.GetAllUsers().ToList();
        }

    }
}
