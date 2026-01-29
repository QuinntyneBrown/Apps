namespace Media.Core.Models;

public class Story
{
    public Guid StoryId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid? PersonId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Story() { }

    public Story(Guid tenantId, string title, string content, Guid? personId = null)
    {
        StoryId = Guid.NewGuid();
        TenantId = tenantId;
        PersonId = personId;
        Title = title;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}
