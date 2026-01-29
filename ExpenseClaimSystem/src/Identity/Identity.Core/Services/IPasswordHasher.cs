namespace Identity.Core.Services;

public interface IPasswordHasher
{
    (string hashedPassword, byte[] salt) HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword, byte[] salt);
}
