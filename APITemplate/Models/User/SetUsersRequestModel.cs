using System;
using System.Collections.Generic;

namespace Levinor.APITemplate.Models.User
{
    public class SetUsersRequestModel
    {
        public Guid Guid { get; set; }
        public List<UserPassModel> users { get; set; }
    }
}
