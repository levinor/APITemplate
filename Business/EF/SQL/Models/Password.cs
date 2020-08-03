using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Levinor.Business.EF.SQL.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string Pass { get; set; }
        public DateTime ExpiringDate { get; set; }
    }
}
