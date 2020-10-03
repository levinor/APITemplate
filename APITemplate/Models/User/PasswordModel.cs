using Levinor.APITemplate.Validators.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class PasswordModel
    {
       
        [PasswordValidator]
        public string NewPassword { get; set; }

    }
}
