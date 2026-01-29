namespace Reflections.Core.Models;

public class Reflection
{
    public Guid ReflectionId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Insights { get; set; }
    public DateTime ReflectionDate { get; set; }
    public bool IsSharedWithPartner { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
