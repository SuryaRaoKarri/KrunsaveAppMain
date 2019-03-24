using System.Collections.Generic;
using System.Threading.Tasks;
using Krunsave.DTO;
using Krunsave.Model;
using Microsoft.AspNetCore.Mvc;

namespace Krunsave.Data.IRepository
{
    public interface IAuthenticationRepository
    {
        

        Task<bool> RegisterUserValidate(UserForRegisterDto userForRegisterDto); 
        Task<bool> Register(UserCheckOTP userCheckOTP);
        Task<UserForLoginDto> Login(UserForLoginDto userForLoginDto);

        Task<bool> UserExists(string username);
    }
}