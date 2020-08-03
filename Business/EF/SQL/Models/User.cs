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
        public int PasswordId { get; set; }
        public virtual Password Password { get; set; }
        public DateTime DateUpdated { get; set; }
        public User UserUpdated { get; set; }
    }
}
