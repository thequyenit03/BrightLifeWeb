using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public required string FirstName { get; set; }   

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public required string LastName { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        [Required]
        public required string PhoneNumber { get; set; } 

        public DateTime DateOfBirth { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public required string Gender { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public required string Address { get; set; }

        [Required]
        public int UserType { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual required ICollection<UserRole> UserRoles { get; set; }
    }
}
