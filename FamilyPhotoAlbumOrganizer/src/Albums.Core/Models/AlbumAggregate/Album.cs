namespace Albums.Core.Models;

public class Album
{
    public Guid AlbumId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? CoverPhotoUrl { get; private set; }

    private Album() { }

    public Album(Guid tenantId, Guid createdByUserId, string name, string? description = null)
    {
        AlbumId = Guid.NewGuid();
        TenantId = tenantId;
        CreatedByUserId = createdByUserId;
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, string? description = null, string? coverPhotoUrl = null)
    {
        if (name != null) Name = name;
        Description = description ?? Description;
        CoverPhotoUrl = coverPhotoUrl ?? CoverPhotoUrl;
    }
}
