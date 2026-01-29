using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Identity.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string hash, byte[] salt) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32));
        return (hash, salt);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        string hashToCompare = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32));
        return hash == hashToCompare;
    }
}
