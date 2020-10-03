using Levinor.Business.EF.SQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Levinor.Business.Repositories.Interfaces
{
    public interface ISQLRepository
    {
        IEnumerable<UserTable> GetAllUsers();
        UserTable GetUserById(int id);
        UserTable GetUserByEmail(string email);
        void UpsertUser(UserTable user);
        void AddUser(UserTable user);
        RoleTable GetRoleById(int id);
        void DeleteUser(UserTable user);
    }
}
