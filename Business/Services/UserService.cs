using AutoMapper;
using Levinor.Business.Domain;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services.Interfaces;
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

        public IEnumerable<User> GetAllUsers() => _repository.GetAllUsers().ToList().Select(x => _mapper.Map<Domain.User>(x));


        public User GetUserById(int Id)
        {
            User response;
            try
            {
                response = _mapper.Map<User>(_repository.GetUserById(Id));

            } catch (Exception e)
            {
                throw new ArgumentException($"User with ID: {Id} does not exist.");
            }

            if (response == null) throw new ArgumentException($"User with ID: {Id} does not exist.");

            return response;
        }

        public Token GetLoginToken(string email, string pass)
        {
            User user;

            // This try catch is only to change the Null Reference of the mapping attemp 
            // into an Argument Exception
            try
            {
                user = _mapper.Map<User>(_repository.GetUserByEmail(email));
            }
            catch
            {
                throw new ArgumentException("Incorrect Password or User");
            }

            string key;

            using (var algorithm = new Rfc2898DeriveBytes(pass, Encoding.ASCII.GetBytes(KeySalt)))
            {
                key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }

            if (user.Password.Pass.Equals(key))
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
        public bool CheckToken(string token)
        {
            User user = new User();
            return _cacheService.checkAuthToken(Guid.Parse(token), out user);
        }

        public void SetNewPassword(string token, string email, string currentPassword, string newPassword)
        {
            User user;
            _cacheService.checkAuthToken(Guid.Parse(token), out user);

            if (user.Email != email )
                throw new ArgumentException("Only logged users can change their password");

            string oldKey;
            using (var algorithm = new Rfc2898DeriveBytes(currentPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                oldKey = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }


            if (user.Password.Pass != oldKey)
                throw new ArgumentException("Current password is incorrect");

            string newKey;
            using (var algorithm = new Rfc2898DeriveBytes(newPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                newKey = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }

            user.Password.Pass = newKey;
            user.Password.ExpiringDate = DateTime.Now.AddMonths(6);
            _repository.UpdateUser(_mapper.Map<EF.SQL.Models.User>(user));
        }

    }
}
