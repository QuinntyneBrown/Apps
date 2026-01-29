namespace Identity.Core.Models;

public class User
{
    public Guid UserId { get; private set; }
    public Guid TenantId { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public bool IsActive { get; private set; }

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    private User() { }

    public static User Create(Guid tenantId, string email, string passwordHash, string firstName, string lastName)
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            TenantId = tenantId,
            Email = email,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}
