namespace Ratings.Core.Models;

public class Rating
{
    public Guid RatingId { get; set; }
    public Guid? DateIdeaId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid UserId { get; set; }
    public int Score { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
