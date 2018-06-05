using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Authentication
{
    [Table("Register")]
    public class Register
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string UserName { get; set; }
        [Column(TypeName = "varchar"), MaxLength(255)]
        public string Email { get; set; }
        [Column(TypeName = "varchar"), MaxLength(5000)]
        public string Password { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string Role { get; set; }
    }
}
