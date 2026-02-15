namespace DateIdeas.Core.Models;

public class DateIdea
{
    public Guid DateIdeaId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal? EstimatedCost { get; set; }
    public string? Duration { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
