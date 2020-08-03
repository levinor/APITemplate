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
    }
}
