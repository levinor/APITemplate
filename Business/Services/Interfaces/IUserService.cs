using Levinor.Business.Domain;
using Levinor.Business.Domain.Responses;
using System;
using System.Collections.Generic;

namespace Levinor.Business.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<GetUserResponse> GetAllUsers();

        GetUserResponse GetUserById(int Id);

        Token GetLoginToken(User user, Password password);

        bool CheckToken(Guid token);

        void SetNewPassword(Guid token, User user, Password password);

        void SetNewUser(Guid token, User userRequest, Password passwordRequest, Role roleRequest);

        void DeleteUser(Guid token, string email);
    }
}
