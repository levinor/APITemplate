using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surename { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{3,}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
