using Levinor.Business.EF.SQL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
    }
}
