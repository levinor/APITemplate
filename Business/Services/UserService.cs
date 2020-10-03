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

        public IEnumerable<GetUserResponse> GetAllUsers()
        {
            List<GetUserResponse> response = new List<GetUserResponse>();
            _repository.GetAllUsers().ToList().ForEach(x => {
                User user = _mapper.Map<Domain.User>(x);
                response.Add(new GetUserResponse
                {
                    User = user,
                    Role = user.Role
                });
                });
            return response;
        }


        public GetUserResponse GetUserById(int Id)
        {
            User user;
            GetUserResponse response = new GetUserResponse();
            try
            {
                user = _mapper.Map<User>(_repository.GetUserById(Id));
                response.User = user;
                response.Role = user.Role;

            } catch (Exception e)
            {
                throw new ArgumentException($"User with ID: {Id} does not exist.");
            }

            if (response == null) throw new ArgumentException($"User with ID: {Id} does not exist.");

            return response;
        }

        public Token GetLoginToken(User userRequest, Password passwordRequest)
        {
            User user;

            // This try catch is only to change the Null Reference of the mapping attemp 
            // into an Argument Exception
            try
            {
                user = _mapper.Map<User>(_repository.GetUserByEmail(userRequest.Email));
            }
            catch(Exception e)
            {
                throw new ArgumentException("Incorrect Password or User");
            }
            if(user == null) throw new ArgumentException("Incorrect Password or User");

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
            User user;
            _cacheService.checkAuthToken(token, out user);

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
            _repository.UpsertUser(_mapper.Map<EF.SQL.Models.UserTable>(user));
        }
        public void SetNewUser(Guid token, User userRequest, Password passwordRequest, Role roleRequest)
        {
            User creator;
            _cacheService.checkAuthToken(token, out creator);

            if (Enum.Parse<UserType>(creator.Role.Name) != UserType.Administrator) throw new ArgumentException("Only Admins can create new users");

            string newKey;
            using (var algorithm = new Rfc2898DeriveBytes(passwordRequest.NewPassword, Encoding.ASCII.GetBytes(KeySalt)))
            {
                newKey = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }

            EF.SQL.Models.UserTable newUser = new EF.SQL.Models.UserTable
            {
                Name = userRequest.Name,
                Surename = userRequest.Surename,
                Email = userRequest.Email,
                DateUpdated = DateTime.Now,
                UserUpdated = _mapper.Map<EF.SQL.Models.UserTable>(creator),
                Password = new EF.SQL.Models.PasswordTable
                {
                    Password = newKey,
                    ExpiringDate = DateTime.Now.AddMonths(6)
                },
                Role = _mapper.Map <EF.SQL.Models.RoleTable>(roleRequest)
            };           

            _repository.AddUser(newUser);
        }

        public void DeleteUser(Guid token, string email)
        {
            User admin;
            _cacheService.checkAuthToken(token, out admin);
            if (Enum.Parse<UserType>(admin.Role.Name) != UserType.Administrator) throw new ArgumentException("Only Admins can delete users");

            UserTable toDelete = _repository.GetUserByEmail(email);
            if (toDelete == null) throw new ArgumentException("User not found");

            _repository.DeleteUser(toDelete);
        }

    }
}
