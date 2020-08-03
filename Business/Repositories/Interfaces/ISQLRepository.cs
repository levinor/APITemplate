using Levinor.Business.EF.SQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Levinor.Business.Repositories.Interfaces
{
    public interface ISQLRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int Id);
    }
}
