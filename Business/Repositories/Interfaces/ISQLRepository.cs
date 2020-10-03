using Levinor.Business.EF.SQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Levinor.Business.Repositories.Interfaces
{
    public interface ISQLRepository
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        UserDto GetUserByEmail(string email);
        void UpsertUser(UserDto user);
        void AddUser(UserDto user);
        RoleDto GetRoleById(int id);
        void DeleteUser(UserDto user);
    }
}
