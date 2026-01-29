namespace Sources.Core.Models;

public class Source
{
    public Guid SourceId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SourceType { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? Url { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
