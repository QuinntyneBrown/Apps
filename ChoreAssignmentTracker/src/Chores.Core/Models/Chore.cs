namespace Chores.Core.Models;

public class Chore
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PointValue { get; set; }
    public string? Frequency { get; set; }
    public int? EstimatedMinutes { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? TenantId { get; set; }
}
