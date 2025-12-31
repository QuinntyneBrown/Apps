namespace BillPaymentScheduler.Core.Services;

public interface IPasswordHasher
{
    (string HashedPassword, byte[] Salt) HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword, byte[] salt);
}
