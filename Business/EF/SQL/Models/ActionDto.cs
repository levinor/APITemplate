using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Levinor.Business.EF.SQL.Models
{
    [Table("Action", Schema ="dbo")]
    public class ActionDto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public UserDto Analist { get; set; }        

        public UserDto Asigned { get; set; }

        public UserDto Target { get; set; }



    }
}
