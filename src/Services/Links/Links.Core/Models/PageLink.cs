namespace Links.Core.Models;

public class PageLink
{
    public Guid PageLinkId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid SourcePageId { get; private set; }
    public Guid TargetPageId { get; private set; }
    public string? LinkText { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PageLink() { }

    public PageLink(Guid tenantId, Guid sourcePageId, Guid targetPageId, string? linkText = null)
    {
        PageLinkId = Guid.NewGuid();
        TenantId = tenantId;
        SourcePageId = sourcePageId;
        TargetPageId = targetPageId;
        LinkText = linkText;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateLinkText(string? linkText)
    {
        LinkText = linkText;
    }
}
