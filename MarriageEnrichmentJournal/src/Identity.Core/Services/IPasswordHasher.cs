namespace Identity.Core.Services;

public interface IPasswordHasher
{
    (string hash, byte[] salt) HashPassword(string password);
    bool VerifyPassword(string password, string hash, byte[] salt);
}
