using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    [Table("UserRoles")]
    public class UserRole:BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; } 

        public virtual User? Users { get; set; } 
        public virtual Role? Roles { get; set; }
    }
}
