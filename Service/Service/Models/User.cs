using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    [Table("Users")]
    public class User:BaseEntity
    {

        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(30)]
        [Phone]
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public bool? Gender { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public  string? Address { get; set; }

        public int? UserType { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public virtual  ICollection<UserRole>? UserRoles { get; set; }
    }
}
