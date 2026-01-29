namespace Gratitudes.Core.Models;

public class Gratitude
{
    public Guid GratitudeId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? Category { get; set; }
    public DateTime GratitudeDate { get; set; }
    public bool IsSharedWithPartner { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
