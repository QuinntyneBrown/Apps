namespace Photos.Core.Models;

public class Photo
{
    public Guid PhotoId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid SessionId { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string? FilePath { get; private set; }
    public int? Rating { get; private set; }
    public string? Tags { get; private set; }
    public string? ExifData { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Photo() { }

    public Photo(Guid tenantId, Guid userId, Guid sessionId, string fileName, string? filePath = null)
    {
        PhotoId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        SessionId = sessionId;
        FileName = fileName;
        FilePath = filePath;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetRating(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");
        Rating = rating;
    }

    public void SetTags(string tags)
    {
        Tags = tags;
    }

    public void SetExifData(string exifData)
    {
        ExifData = exifData;
    }
}
