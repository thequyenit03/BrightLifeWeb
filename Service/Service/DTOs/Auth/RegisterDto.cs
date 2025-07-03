using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName   { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(100)]    
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
