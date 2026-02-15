namespace Revisions.Core.Models;

public class PageRevision
{
    public Guid PageRevisionId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid PageId { get; private set; }
    public Guid UserId { get; private set; }
    public int VersionNumber { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public string? ChangeDescription { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PageRevision() { }

    public PageRevision(Guid tenantId, Guid pageId, Guid userId, int versionNumber, string content, string? changeDescription = null)
    {
        PageRevisionId = Guid.NewGuid();
        TenantId = tenantId;
        PageId = pageId;
        UserId = userId;
        VersionNumber = versionNumber;
        Content = content;
        ChangeDescription = changeDescription;
        CreatedAt = DateTime.UtcNow;
    }
}
