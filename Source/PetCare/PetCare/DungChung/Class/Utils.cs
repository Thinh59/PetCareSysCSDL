using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// (Thêm class này vào dự án của bạn, ví dụ: file Utilities.cs)
using System.Security.Cryptography;

public static class PasswordHasher
{
    // Hàm băm (hash) mật khẩu bằng SHA256 (Ví dụ đơn giản)
    public static string Hash(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}