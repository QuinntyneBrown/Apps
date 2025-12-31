using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MorningRoutineBuilder.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    public (string HashedPassword, byte[] Salt) HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        string hashedPassword = HashPasswordWithSalt(password, salt);

        return (hashedPassword, salt);
    }

    public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        if (string.IsNullOrEmpty(hashedPassword) || salt == null || salt.Length == 0)
            return false;

        string computedHash = HashPasswordWithSalt(password, salt);
        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(computedHash),
            Convert.FromBase64String(hashedPassword));
    }

    private static string HashPasswordWithSalt(string password, byte[] salt)
    {
        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations,
            numBytesRequested: HashSize);

        return Convert.ToBase64String(hash);
    }
}
