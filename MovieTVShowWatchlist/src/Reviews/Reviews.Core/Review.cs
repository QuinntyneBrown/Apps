namespace Reviews.Core;

public class Review
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public bool ContainsSpoilers { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
