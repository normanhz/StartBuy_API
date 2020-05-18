using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARTBUY_API.Classes
{
    public class Encrypter
    {
        public static string Encrypt(string encryptpassword)
        {
            string result = string.Empty;
            byte[] encrypted =
            System.Text.Encoding.Unicode.GetBytes(encryptpassword);
            result = Convert.ToBase64String(encrypted);
            return result;
        }

        public static string Decrypt(string decryptpassword)
        {
            string result = string.Empty;
            byte[] decrypted =
            Convert.FromBase64String(decryptpassword);
            System.Text.Encoding.Unicode.GetString(decrypted, 0, decrypted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decrypted);
            return result;
        }
    }
}
