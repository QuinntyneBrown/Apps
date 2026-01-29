using System.Security.Cryptography;
using System.Text;

namespace Identity.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string hash, byte[] salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(32);
        var hash = ComputeHash(password, salt);
        return (hash, salt);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var computedHash = ComputeHash(password, salt);
        return hash == computedHash;
    }

    private static string ComputeHash(string password, byte[] salt)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);
        var hashBytes = sha256.ComputeHash(saltedPassword);
        return Convert.ToBase64String(hashBytes);
    }
}
