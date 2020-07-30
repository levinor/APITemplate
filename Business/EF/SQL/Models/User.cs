using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.EF.SQL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
