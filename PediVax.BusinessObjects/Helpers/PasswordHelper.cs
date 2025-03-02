using System;
using System.Security.Cryptography;
using System.Text;

namespace PediVax.BusinessObjects.Helpers;

public static class PasswordHelper
{
    public static string GenerateSalt()
    {
        byte[] saltBytes = new byte[32]; // Tạo salt 16 byte
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes); // Tạo số ngẫu nhiên
        }
        return Convert.ToBase64String(saltBytes); // Chuyển đổi sang chuỗi base64
    }

    public static string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var hashBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256).GetBytes(32);
        return Convert.ToBase64String(hashBytes);
    }
}