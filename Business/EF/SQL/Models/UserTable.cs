using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Levinor.Business.EF.SQL.Models
{
    [Table("User", Schema = "dbo")]
    public class UserTable
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

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual RoleTable Role { get; set; }

        public int PasswordId { get; set; }

        [ForeignKey("PasswordId")]
        public virtual PasswordTable Password { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }

        public UserTable UserUpdated { get; set; }
    }
}
