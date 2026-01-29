namespace Courses.Core.Models;

public class Course
{
    public Guid CourseId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public int NumberOfHoles { get; set; } = 18;
    public int Par { get; set; } = 72;
    public decimal? CourseRating { get; set; }
    public int? SlopeRating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
