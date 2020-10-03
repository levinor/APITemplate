using Levinor.Business.Domain;
using System;

namespace Levinor.Business.Services.Interfaces
{
    public interface ICacheService
    {
        Guid generateUserToken(User user);
        bool checkAuthToken(Guid token, out User user);
    }
}
