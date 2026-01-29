namespace Reviews.Core.Models;
public class Review { public Guid ReviewId { get; set; } public Guid UserId { get; set; } public DateTime WeekStartDate { get; set; } public string? Summary { get; set; } public int? OverallRating { get; set; } public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
