using Levinor.Business.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Levinor.Business.EF.SQL.Models
{
    [Table("User", Schema = "dbo")]
    public class UserDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surename { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public UserType Role { get; set; }

        public int PasswordId { get; set; }

        [ForeignKey("PasswordId")]
        public virtual PasswordDto Password { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }

        public UserDto Updater { get; set; }

        public UserDto Supervisor { get; set; }

        public bool Active { get; set; }
    }
}
