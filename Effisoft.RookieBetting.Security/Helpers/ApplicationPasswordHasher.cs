using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security.Helpers
{
    public class ApplicationPasswordHasher : IPasswordHasher
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 24;
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        public string HashPassword(string password)
        {
            return CreateHashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            char[] delimiter = { ':' };
            var split = hashedPassword.Split(delimiter);
            var iterations = int.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = Pbkdf2(providedPassword, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        public int VerifyHashedPwd(string hasehdPassword, string providedPassword)
        {
            return (int) VerifyHashedPassword(hasehdPassword, providedPassword);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };
            return pbkdf2.GetBytes(outputBytes);
        }

        private static string CreateHashPassword(string password)
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            var hash = Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }
    }
}
