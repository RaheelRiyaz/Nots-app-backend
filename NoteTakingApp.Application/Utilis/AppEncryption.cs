using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Utilis
{
    public class AppEncryption
    {

        public static string GenerateSalt()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(bytes);
        }


        public static string GenerateHashedPassword(string password, string salt)
        {
            var passwordWithSalt = String.Concat(password, salt);

            var base64Pass = Convert.ToBase64String(Encoding.UTF8.GetBytes(passwordWithSalt));

            var hmacsha = SHA256.Create();

            var computedPass = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(base64Pass));

            return Convert.ToBase64String(computedPass);
        }



        public static bool ComparePassword(string dbPass , string userPass, string userSalt)
        {
            return dbPass == GenerateHashedPassword(userPass, userSalt);
        }
    }
}
