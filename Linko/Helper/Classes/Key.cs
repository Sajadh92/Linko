using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Linko.Helper
{
    public static class Key
    {
        public static readonly string UserProfile = "UserProfile";

        public static readonly string SecretKey = "cdAsuIt+MtEDbT5p9I7o3TvBgteF+4l/2sFpWG4Hloi7Tre6Dsw3QBYTY8xbWva8GlKgJzQZdcR3Luqm/bgt5u==";
        
        public static DateTime DateTimeIQ => DateTime.UtcNow.AddHours(3);

        public static readonly Dictionary<string, string> Lookup = new()
        {
            { UserProfile , "Linko001" }
        };

        public static string GetHashString(string inputString)
        {
            using HashAlgorithm algorithm = SHA256.Create();
            byte [] inputStringByte = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            StringBuilder sb = new();

            foreach (byte b in inputStringByte)
                sb.Append(b.ToString("X2"));

            string test = BitConverter.ToString(inputStringByte).Replace("-", string.Empty);

            return sb.ToString();
        }
    }
}
