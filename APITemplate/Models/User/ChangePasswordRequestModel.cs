using Levinor.APITemplate.Validators.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class ChangePasswordRequestModel
    {
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{3,}$",
            ErrorMessage = "Incorrect email format")]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(1000)]
        [PasswordValidator]
        public string NewPassword { get; set; }
    }
}
