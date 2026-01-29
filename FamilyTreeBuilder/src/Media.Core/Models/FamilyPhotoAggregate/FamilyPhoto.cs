namespace Media.Core.Models;

public class FamilyPhoto
{
    public Guid FamilyPhotoId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid? PersonId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime? TakenDate { get; private set; }
    public DateTime UploadedAt { get; private set; }

    private FamilyPhoto() { }

    public FamilyPhoto(Guid tenantId, string title, string url, Guid? personId = null, DateTime? takenDate = null)
    {
        FamilyPhotoId = Guid.NewGuid();
        TenantId = tenantId;
        Title = title;
        Url = url;
        PersonId = personId;
        TakenDate = takenDate;
        UploadedAt = DateTime.UtcNow;
    }
}
