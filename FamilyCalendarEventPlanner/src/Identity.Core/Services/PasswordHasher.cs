using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Identity.Core.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 100000;
    private static readonly KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;

    public (string hashedPassword, byte[] salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: Prf,
            iterationCount: Iterations,
            numBytesRequested: KeySize
        );

        return (Convert.ToBase64String(hash), salt);
    }

    public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
    {
        var hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: Prf,
            iterationCount: Iterations,
            numBytesRequested: KeySize
        );

        return Convert.ToBase64String(hash) == hashedPassword;
    }
}
