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
     ErrorMessage = "Characters are not allowed.")]
        public string email { get; set; }

        [Required]
        [StringLength(1000)]
        public string currentPassword { get; set; }

        [Required]
        [StringLength(1000)]
        [PasswordValidator]
        public string newPassword { get; set; }
    }
}
