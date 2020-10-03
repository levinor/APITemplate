﻿using Levinor.APITemplate.Validators.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class SetNewUserRequestModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surename { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{3,}$",
         ErrorMessage = "Incorrect email format")]
        [Required]
        public string Email { get; set; }
        
        [PasswordValidator]
        public string NewPassword { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id needs to begreater than 0")]
        public int RoleId { get; set; }

    }
}
