using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Models.User
{
    public class RoleModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id needs to begreater than 0")]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
