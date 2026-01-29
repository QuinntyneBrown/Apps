using Identity.Core.Models.UserAggregate.Entities;

namespace Identity.Core.Models.UserAggregate;

public class User
{
    private readonly List<UserRole> _userRoles = new();

    public Guid UserId { get; private set; }
    public Guid TenantId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public byte[] Salt { get; private set; } = Array.Empty<byte>();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private User() { }

    public User(Guid tenantId, string userName, string email, string hashedPassword, byte[] salt)
    {
        UserId = Guid.NewGuid();
        TenantId = tenantId;
        UserName = userName;
        Email = email;
        Password = hashedPassword;
        Salt = salt;
    }

    public void UpdateProfile(string? userName = null, string? email = null)
    {
        if (userName != null) UserName = userName;
        if (email != null) Email = email;
    }

    public void SetPassword(string hashedPassword, byte[] salt)
    {
        Password = hashedPassword;
        Salt = salt;
    }

    public void AddRole(Role role)
    {
        if (_userRoles.Any(ur => ur.RoleId == role.RoleId)) return;
        _userRoles.Add(new UserRole(TenantId, UserId, role.RoleId));
    }

    public void RemoveRole(Guid roleId)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole != null) _userRoles.Remove(userRole);
    }
}
