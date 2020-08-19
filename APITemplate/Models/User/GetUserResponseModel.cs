using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class GetUserResponseModel
    {
        public UserModel User { get; set; }
        public RoleModel Role { get; set; }
    }
}
