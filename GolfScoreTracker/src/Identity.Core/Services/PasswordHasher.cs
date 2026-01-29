using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Identity.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 100000;

    public (string hash, byte[] salt) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations, numBytesRequested: KeySize));
        return (hash, salt);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        string hashToCompare = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations, numBytesRequested: KeySize));
        return hash == hashToCompare;
    }
}
