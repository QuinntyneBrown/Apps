namespace Albums.Core.Models;

public class Photo
{
    public Guid PhotoId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid AlbumId { get; private set; }
    public Guid UploadedByUserId { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public string? Caption { get; private set; }
    public DateTime TakenAt { get; private set; }
    public DateTime UploadedAt { get; private set; }

    private Photo() { }

    public Photo(Guid tenantId, Guid albumId, Guid uploadedByUserId, string fileName, string url, DateTime? takenAt = null, string? caption = null)
    {
        PhotoId = Guid.NewGuid();
        TenantId = tenantId;
        AlbumId = albumId;
        UploadedByUserId = uploadedByUserId;
        FileName = fileName;
        Url = url;
        TakenAt = takenAt ?? DateTime.UtcNow;
        Caption = caption;
        UploadedAt = DateTime.UtcNow;
    }

    public void Update(string? caption = null) => Caption = caption ?? Caption;
}
