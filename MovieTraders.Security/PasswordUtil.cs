using System;
using System.Linq;
using System.Security.Cryptography;

namespace MovieTraders.Security
{
    public class PasswordUtil
    {
        public static PasswordDetails Generate(string password)
        {
            byte[] salt = new byte[16];
            int iterations = new Random().Next(1000, 10000);
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            provider.GetBytes(salt);
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = deriveBytes.GetBytes(16);
            return new PasswordDetails(salt, hash, iterations);
        }

        public static bool IsMatch(string password, byte[] salt, byte[] hash, int iterations)
        {
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] test = deriveBytes.GetBytes(16);
            return test.SequenceEqual(hash);
        }
    }
}
