using System;
using System.Security.Cryptography;
using System.Text;

namespace Persada.Fr.CommonFunction
{
    class Hashing
    {
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string password)
        {
            //string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string CreatePasswordAndSaltHash(string hashpassword, string salt)
        {
            string pwdAndSalt = String.Concat(hashpassword, salt);
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(pwdAndSalt));
            StringBuilder sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
