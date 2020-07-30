using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.EF.SQL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
