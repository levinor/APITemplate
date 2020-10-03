using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Levinor.Business.EF.SQL.Models
{
    [Table("Password", Schema = "dbo")]
    public class PasswordTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PasswordId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }
        

        [ForeignKey("UserId")]
        public UserTable User { get; set; }

        public DateTime ExpiringDate { get; set; }

    }
}
