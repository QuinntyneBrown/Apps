namespace Pages.Core.Models;

public class WikiPage
{
    public Guid WikiPageId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public string Status { get; private set; } = "Draft";
    public Guid? CategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    private WikiPage() { }

    public WikiPage(Guid tenantId, Guid userId, string title, string slug, string content, Guid? categoryId = null)
    {
        WikiPageId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Title = title;
        Slug = slug;
        Content = content;
        CategoryId = categoryId;
        Status = "Draft";
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string content, Guid? categoryId = null)
    {
        Title = title;
        Content = content;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        Status = "Published";
        PublishedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = "Archived";
        UpdatedAt = DateTime.UtcNow;
    }
}
