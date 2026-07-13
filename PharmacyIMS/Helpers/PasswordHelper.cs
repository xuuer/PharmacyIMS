using System;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyIMS.Helpers
{
    /// <summary>
    /// 密码加密辅助类，使用 SHA256 哈希
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// 对密码进行 SHA256 哈希
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 验证密码是否匹配
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
