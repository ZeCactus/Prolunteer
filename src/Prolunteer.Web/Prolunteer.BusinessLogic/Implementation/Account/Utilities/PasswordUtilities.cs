using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Account.Utilities
{
    public class PasswordUtilities
    {
        public static readonly int SaltSize = 16;
        public static readonly int HashSize = 28;
        public static readonly int iterations = 1000;
        private static byte[] NewSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
            return salt;
        }

        public static string Hash(string password)
        {
            var salt = NewSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            var base64Hash = Convert.ToBase64String(hash);
            var base64Salt = Convert.ToBase64String(salt);

            var result = string.Format($"{base64Hash}${base64Salt}");
            return result;
        }

        public static bool CheckPassword(string password, string passwordHash)
        {
            var hash = passwordHash.Split("$")[0];
            var salt = passwordHash.Split("$")[1];

            var hashBytes = Convert.FromBase64String(hash);
            var saltBytes = Convert.FromBase64String(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            var hashToCheck = pbkdf2.GetBytes(HashSize);

            return Enumerable.SequenceEqual(hashBytes, hashToCheck);
        }

    }
}
