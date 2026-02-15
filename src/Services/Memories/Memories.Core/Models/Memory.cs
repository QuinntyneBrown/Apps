namespace Memories.Core.Models;

public class Memory
{
    public Guid MemoryId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? BucketListItemId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Location { get; private set; }
    public DateTime MemoryDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Memory() { }

    public Memory(Guid tenantId, Guid userId, string title, DateTime memoryDate, Guid? bucketListItemId = null, string? description = null, string? location = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        MemoryId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BucketListItemId = bucketListItemId;
        Title = title;
        Description = description;
        Location = location;
        MemoryDate = memoryDate;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? title = null, string? description = null, string? location = null, DateTime? memoryDate = null)
    {
        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        if (description != null)
            Description = description;

        if (location != null)
            Location = location;

        if (memoryDate.HasValue)
            MemoryDate = memoryDate.Value;
    }
}
