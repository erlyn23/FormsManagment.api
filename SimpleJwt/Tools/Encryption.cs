using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Morocoto.Infraestructure.Tools
{
    public class Encryption
    {
        public static string Encrypt(string stringToEncrypt)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder stringBuilder = new StringBuilder();
            var stream = sha256.ComputeHash(encoding.GetBytes(stringToEncrypt));
            for (int i = 0; i < stream.Length; i++) stringBuilder.AppendFormat("{0:x2}", stream[i]);
            return stringBuilder.ToString();
        }
    }
}
