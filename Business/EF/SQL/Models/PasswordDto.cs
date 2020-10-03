using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Levinor.Business.EF.SQL.Models
{
    [Table("Password", Schema = "dbo")]
    public class PasswordDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PasswordId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }
        

        [ForeignKey("UserId")]
        public UserDto User { get; set; }

        public DateTime ExpiringDate { get; set; }

    }
}
