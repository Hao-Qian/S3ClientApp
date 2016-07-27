using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace S3ClientApp
{
    public static class MD5Utilities
    {
        public static string GenerateMD5(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                return GetMD5HashFromStream(stream);
            }
        }

        private static string GetMD5HashFromStream(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(stream);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
