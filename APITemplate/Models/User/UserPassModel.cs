using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class UserPassModel
    {
        public UserModel user { get; set; }
        [StringLength(1000)]
        public string Password { get; set; }
    }
}
