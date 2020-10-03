using Levinor.Business.Domain;
using Levinor.Business.Domain.Responses;
using System;
using System.Collections.Generic;

namespace Levinor.Business.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(int Id);

        Token GetLoginToken(User user, Password password);

        bool CheckToken(Guid token);

        void SetNewPassword(Guid token, User user, Password password);

        void SetNewUser(Guid token, User userRequest, Password passwordRequest);

        void DeactiveUser(Guid token, string email);

    }
}
