namespace Experiences.Core.Models;

public class Experience
{
    public Guid ExperienceId { get; set; }
    public Guid DateIdeaId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExperienceDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal? ActualCost { get; set; }
    public string? Photos { get; set; }
    public bool WasSuccessful { get; set; }
    public bool WouldRepeat { get; set; }
    public DateTime CreatedAt { get; set; }
}
