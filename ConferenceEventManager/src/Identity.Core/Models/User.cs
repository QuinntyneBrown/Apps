namespace Identity.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? TenantId { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
