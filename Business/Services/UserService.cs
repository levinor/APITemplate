using AutoMapper;
using Levinor.Business.Domain;
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
        private readonly IMapper _mapper;
        public UserService(ISQLRepository repository,
             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public IEnumerable<User> GetAllUsers() => _repository.GetAllUsers().ToList().Select(x => _mapper.Map<Domain.User>(x));           
        

        public User GetUserById(int Id)
        {
            User response = _mapper.Map<User>(_repository.GetUserById(Id));
            if (response == null) throw new ArgumentException($"User with ID: {Id} does not exist.");

            return response;
        }
    }
}
