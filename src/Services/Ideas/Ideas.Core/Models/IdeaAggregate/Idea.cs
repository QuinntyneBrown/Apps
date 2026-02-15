namespace Ideas.Core.Models;

public class Idea
{
    public Guid IdeaId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid RecipientId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? EstimatedPrice { get; set; }
    public string? Url { get; set; }
    public string? Category { get; set; }
    public int Priority { get; set; } = 1;
    public bool IsPurchased { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
