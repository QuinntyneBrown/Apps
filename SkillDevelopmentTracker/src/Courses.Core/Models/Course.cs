namespace Courses.Core.Models;

public class Course
{
    public Guid CourseId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Provider { get; private set; }
    public string? Url { get; private set; }
    public int? DurationHours { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Course() { }

    public Course(Guid tenantId, Guid userId, string title, string? provider = null, string? url = null, int? durationHours = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        CourseId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Title = title;
        Provider = provider;
        Url = url;
        DurationHours = durationHours;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? title = null, string? provider = null, string? url = null, int? durationHours = null)
    {
        if (title != null) Title = title;
        if (provider != null) Provider = provider;
        if (url != null) Url = url;
        if (durationHours.HasValue) DurationHours = durationHours;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
}
