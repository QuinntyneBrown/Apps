namespace Lessons.Core.Models;

public class Lesson
{
    public Guid LessonId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid? SourceId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Tags { get; set; }
    public DateTime DateLearned { get; set; }
    public string? Application { get; set; }
    public bool IsApplied { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void MarkAsApplied()
    {
        IsApplied = true;
    }
}
