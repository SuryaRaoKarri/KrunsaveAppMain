using System.ComponentModel.DataAnnotations;

namespace Krunsave.DTO
{
    public class UserCheckOTP
    {
        [Required]
        public string email {get; set;}
        [Required]
        public string password {get; set;}
        [Required]
        public int otp {get; set;}
    }
}