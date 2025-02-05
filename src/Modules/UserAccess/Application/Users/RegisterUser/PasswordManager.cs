using System;
using System.Security.Cryptography;

namespace Promomash.Trader.UserAccess.Application.Users.RegisterUser
{
    public class PasswordManager
    {
        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            using var rfc2898 = new Rfc2898DeriveBytes(password, 0x10, 0x3e8, HashAlgorithmName.SHA256);
            
            var salt = rfc2898.Salt;
            var hash = rfc2898.GetBytes(0x20);

            var result = new byte[0x31];
            Buffer.BlockCopy(salt, 0, result, 1, salt.Length);
            Buffer.BlockCopy(hash, 0, result, 0x11, hash.Length);
                
            return Convert.ToBase64String(result);
        }
    }
}