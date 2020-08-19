using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.Domain.Responses
{
    public class GetUserResponse
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
