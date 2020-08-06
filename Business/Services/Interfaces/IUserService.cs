using Levinor.Business.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        
        User GetUserById(int Id);

        Token GetLoginToken(string email, string pass);

        bool CheckToken(string token);

        void SetNewPassword(string token, string email, string currentPassword, string newPassword);
    }
}
