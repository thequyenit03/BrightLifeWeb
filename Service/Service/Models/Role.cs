using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    [Table("Roles")]
    public class Role:BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]
        public string? Description { get; set; }


        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
