using System;
using System.Security.Cryptography;
using System.Text;

namespace Cleverbit.Framework.Helper
{
    public static class CommonHelper
    {
        public static string CreateSalt()
        {
            var saltBytes = new byte[64];

            var rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string EncodePassword(string password, string passwordSalt)
        {
            byte[] bytePassword = Encoding.Unicode.GetBytes(password);
            byte[] bytePasswordSalt = Convert.FromBase64String(passwordSalt);
            var byteBuffer = new byte[bytePasswordSalt.Length + bytePassword.Length];
            Buffer.BlockCopy(bytePasswordSalt, 0, byteBuffer, 0, bytePasswordSalt.Length);
            Buffer.BlockCopy(bytePassword, 0, byteBuffer, bytePasswordSalt.Length, bytePassword.Length);
            byte[] byteEncryptSha1 = new SHA1CryptoServiceProvider().ComputeHash(byteBuffer);
            return Convert.ToBase64String(byteEncryptSha1);
        }
    }
}
