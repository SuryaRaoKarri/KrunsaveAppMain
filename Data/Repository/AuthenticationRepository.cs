using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Krunsave.Data.IRepository;
using Krunsave.DTO;
using Krunsave.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Krunsave.Data.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
         //Database
        private readonly DataContext _context;
        public AuthenticationRepository(DataContext context){
            _context = context;
        }
       
        public async Task<UserForLoginDto> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.email == userForLoginDto.email);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.roleID == user.roleID);

            if(user == null) return null; // Will return 401 unauthorize in the controller

            if(!VerifyPasswordHash(userForLoginDto.password, user.passwordHash, user.passwordSalt)) return null;

            userForLoginDto.userID = user.userID;
            userForLoginDto.userName = user.userName;
            userForLoginDto.roleName = role.roleName;
            return userForLoginDto;
        }

        public async Task<bool> RegisterUserValidate(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            int otp;
            
            //Delete the user if it exists in the Userregistervalidate table
            var delUser = await _context.Userregistervalidates.FirstOrDefaultAsync(u => u.email == userForRegisterDto.email);
            if(delUser != null) _context.Userregistervalidates.Remove(delUser);
            
            //Convert the Password to Hash Password
            CreatePasswordHash(userForRegisterDto.password, out passwordHash, out passwordSalt);
            //Get the roleID
            var roles = await _context.Roles.FirstOrDefaultAsync(r => r.roleName == userForRegisterDto.rolename);
            if(roles != null){
                //Check the verification code is send via email
                if(!SendMailOTP(out otp, userForRegisterDto.email)) return false;

                //Update the User Model
                var user = new Userregistervalidate();
                user.email = userForRegisterDto.email;
                user.userName = userForRegisterDto.userName;
                user.phoneNumber = userForRegisterDto.phoneNumber;
                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;
                user.otp = otp;
                user.roleID = roles.roleID;
                await _context.Userregistervalidates.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

         public async Task<bool> Register(UserCheckOTP userCheckOTP)
        {
            var userdetails = await _context.Userregistervalidates.FirstOrDefaultAsync(u => u.email == userCheckOTP.email);
            if(userdetails == null) return false;

            if(! VerifyPasswordHash(userCheckOTP.password, userdetails.passwordHash, userdetails.passwordSalt)) return false;
             var user = new User();
             user.email = userdetails.email;
             user.userName = userdetails.userName;
             user.passwordHash = userdetails.passwordHash;
             user.passwordSalt = userdetails.passwordSalt;
             user.phoneNumber = userdetails.phoneNumber;
             user.roleID = userdetails.roleID;

             await _context.Users.AddAsync(user);
             _context.Userregistervalidates.Remove(userdetails);
             _context.SaveChanges();
            
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
  
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i< ComputeHash.Length; i++)
                {
                    if(ComputeHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private bool SendMailOTP(out int otp, string email){
            Random generator = new Random();
            otp = generator.Next(100000, 1000000);
            try{
                string fromaddr = "";
                string password = "";
                MailMessage msg = new MailMessage();
                msg.Subject = "Krunsave Registration Verification Code";
                msg.From = new MailAddress(fromaddr);
                msg.Body = otp.ToString();
                msg.To.Add(new MailAddress(email));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential(fromaddr,password);
                smtp.Credentials = nc;
                smtp.Send(msg);
                
                return true;
            }
            catch{
                return false;
            }

        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.email == username)) return true;

            return false;
        }

       
    }
}