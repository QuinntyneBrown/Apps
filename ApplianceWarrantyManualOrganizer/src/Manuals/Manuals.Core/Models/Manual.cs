namespace Manuals.Core.Models;

public class Manual
{
    public Guid ManualId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ApplianceId { get; set; }
    public string? Title { get; set; }
    public string? FileUrl { get; set; }
    public string? FileType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
