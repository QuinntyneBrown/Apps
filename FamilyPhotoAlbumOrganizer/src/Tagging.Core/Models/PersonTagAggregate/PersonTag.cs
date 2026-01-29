namespace Tagging.Core.Models;

public class PersonTag
{
    public Guid PersonTagId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid PhotoId { get; private set; }
    public string PersonName { get; private set; } = string.Empty;
    public int? XPosition { get; private set; }
    public int? YPosition { get; private set; }

    private PersonTag() { }

    public PersonTag(Guid tenantId, Guid photoId, string personName, int? xPosition = null, int? yPosition = null)
    {
        PersonTagId = Guid.NewGuid();
        TenantId = tenantId;
        PhotoId = photoId;
        PersonName = personName;
        XPosition = xPosition;
        YPosition = yPosition;
    }

    public void Update(string? personName = null, int? xPosition = null, int? yPosition = null)
    {
        if (personName != null) PersonName = personName;
        XPosition = xPosition ?? XPosition;
        YPosition = yPosition ?? YPosition;
    }
}
