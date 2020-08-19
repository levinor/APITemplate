using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.Domain
{
    public class Password
    {
        public int PasswordId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public DateTime ExpiringDate { get; set; }
    }
}
