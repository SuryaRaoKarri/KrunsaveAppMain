using System.ComponentModel.DataAnnotations;

namespace Krunsave.DTO
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress]
        public string email {get; set;}
        [Required]
        public string userName {get; set;}
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 16")]
        public string password {get; set;}
        [Required]
        public string phoneNumber {get; set;}
        [Required]
        public string rolename {get; set;}
    }
}