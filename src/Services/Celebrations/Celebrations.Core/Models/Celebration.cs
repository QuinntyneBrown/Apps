namespace Celebrations.Core.Models;

public class Celebration
{
    public Guid CelebrationId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CelebrationType { get; set; } = string.Empty; // Birthday, Anniversary, etc.
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
