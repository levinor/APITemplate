using AutoMapper;
using Levinor.Business.Domain;
using Levinor.Business.Domain.Responses;
using Levinor.Business.EF.SQL.Models;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services.Interfaces;
using Levinor.Business.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Levinor.Business.Services
{
    public class UserService : IUserService
    {
        private ISQLRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private const int KeySize = 32;
        private const string KeySalt = "8%b4D%Xs&g&NQX7aHQTRLX;u&!j7ata5B$?HOiDR/)xS.2E%)l6D!__&DCVIjqvB/UEMDCLOCpj)-5nWnF8eOA=4-8&5/oKW-T¿!Z9¿hO9gROOU.=O4i5B%QoRmd48L0AEw58!9Vb8InpJ0f¿S;q.7?0%F&a_selumXkf2Fb$P8V)(eqOr)e_lzKJ4BdX2BdDUpyALIMaH?AlEQMJO,13re3wRbxfqKYmHWPVa6RGxTWHrXTnn?F5UOr!8iB8-Fc";

        public UserService(ISQLRepository repository,
             IMapper mapper,
             ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cache;
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> response = new List<User>();
            GetAllUsers().ToList().ForEach(x => {

                response.Add(_mapper.Map<User>(x));
                });
            return response;
        }


        public User GetUserById(int Id)
        {
            UserDto userTable = _repository.GetUserById(Id);
            if (userTable == null) throw new ArgumentException($"User with ID: {Id} does not exist.");

            return _mapper.Map<User>(userTable);   
        }

        public Token GetLoginToken(User userRequest, Password passwordRequest)
        {
            UserDto userTable = _repository.GetUserByEmail(userRequest.Email);
            if (userTable == null) throw new ArgumentException("Incorrect Password or User");

            User user = _mapper.Map<User>(userTable);

            string key;
            using (var algorithm = new Rfc2898DeriveBytes(passwordRequest.CurrentPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }

            if (user.Password.CurrentPassword.Equals(key))
            {
                return new Token {
                    token = _cacheService.generateUserToken(user),
                    needsToBeUpdated = user.Password.ExpiringDate.CompareTo(DateTime.Now) <= 0
                };
            }
            else
            {
                throw new ArgumentException("Incorrect Password or User");
            }
        }
        public bool CheckToken(Guid token)
        {
            return _cacheService.checkAuthToken(token, out _);
        }

        public void SetNewPassword(Guid token, User userRequest, Password passwordRequest)
        {
            _cacheService.checkAuthToken(token, out User user);

            if (user.Email != userRequest.Email)
                throw new ArgumentException("Only logged users can change their password");

            string oldKey;
            using (var algorithm = new Rfc2898DeriveBytes(passwordRequest.CurrentPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                oldKey = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }


            if (user.Password.CurrentPassword != oldKey)
                throw new ArgumentException("Current password is incorrect");

            string newKey;
            using (var algorithm = new Rfc2898DeriveBytes(passwordRequest.NewPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                newKey = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }

            user.Password.CurrentPassword = newKey;
            user.Password.ExpiringDate = DateTime.Now.AddMonths(6);
            _repository.UpsertUser(_mapper.Map<EF.SQL.Models.UserDto>(user));
        }
        public void SetNewUser(Guid token, User userRequest, Password passwordRequest)
        {
            _cacheService.checkAuthToken(token, out User creator);

            if ((creator.Role) < UserType.RegionalManager) throw new ArgumentException("Only Managers can create new users");

            string newKey;
            using (var algorithm = new Rfc2898DeriveBytes(passwordRequest.NewPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                newKey = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }

            EF.SQL.Models.UserDto newUser = new EF.SQL.Models.UserDto
            {
                Name = userRequest.Name,
                Surename = userRequest.Surename,
                Email = userRequest.Email,
                DateUpdated = DateTime.Now,
                Updater = _mapper.Map<EF.SQL.Models.UserDto>(creator),
                Password = new EF.SQL.Models.PasswordDto
                {
                    Password = newKey,
                    ExpiringDate = DateTime.Now.AddMonths(6)
                },
                Role = userRequest.Role
            };           

            _repository.AddUser(newUser);
        }

        public void DeactiveUser(Guid token, string email)
        {
            _cacheService.checkAuthToken(token, out User CEO);
            if ((CEO.Role) < UserType.CEO) throw new ArgumentException("Only the CEO can deactivate users");

            UserDto toDelete = _repository.GetUserByEmail(email);
            if (toDelete == null) throw new ArgumentException("User not found");

            //Generate action in the internal project
            
            _repository.DeleteUser(toDelete);
        }

    }
}
