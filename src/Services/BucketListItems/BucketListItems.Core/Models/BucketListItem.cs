namespace BucketListItems.Core.Models;

public class BucketListItem
{
    public Guid BucketListItemId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public int Priority { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? TargetDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private BucketListItem() { }

    public BucketListItem(Guid tenantId, Guid userId, string title, string category, int priority = 1, string? description = null, DateTime? targetDate = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be empty.", nameof(category));
        if (priority < 1)
            throw new ArgumentException("Priority must be at least 1.", nameof(priority));

        BucketListItemId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Title = title;
        Category = category;
        Priority = priority;
        Description = description;
        TargetDate = targetDate;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? title = null, string? description = null, string? category = null, int? priority = null, DateTime? targetDate = null)
    {
        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        if (description != null)
            Description = description;

        if (category != null)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be empty.", nameof(category));
            Category = category;
        }

        if (priority.HasValue)
        {
            if (priority.Value < 1)
                throw new ArgumentException("Priority must be at least 1.", nameof(priority));
            Priority = priority.Value;
        }

        if (targetDate.HasValue)
            TargetDate = targetDate.Value;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
}
