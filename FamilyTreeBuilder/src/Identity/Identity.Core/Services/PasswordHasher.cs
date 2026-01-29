using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Identity.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string hashedPassword, byte[] salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32);
        return (Convert.ToBase64String(hash), salt);
    }

    public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
    {
        var hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32);
        return Convert.ToBase64String(hash) == hashedPassword;
    }
}
