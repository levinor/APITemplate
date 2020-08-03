using Levinor.Business.EF.SQL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Levinor.Business.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }             
        public Password Password { get; set; }
    }
}
