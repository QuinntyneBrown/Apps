namespace Ratings.Core;

public class Rating
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
