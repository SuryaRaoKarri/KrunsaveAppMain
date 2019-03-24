using System.ComponentModel.DataAnnotations;

namespace Krunsave.Model
{
    public class Userregistervalidate
    {
        [Key]
        public int usertempID {get; set;}
        [Required]
        public string userName {get; set;}
        [Required]
        public string email {get; set;}
        [Required]
        public byte[] passwordHash {get; set;}
        [Required]
        public byte[] passwordSalt {get; set;}
        public string phoneNumber {get; set;}
        [Required]
        public int otp {get; set;}
        public Role role {get; set;}
        public int roleID {get; set;}
    }
}