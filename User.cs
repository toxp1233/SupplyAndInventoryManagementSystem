using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMS.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHush { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        
        public bool CanGrantAdminRights { get; set; } = false;

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
