namespace Notes.Core.Models;

public class Note
{
    public Guid NoteId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Note() { }

    public Note(Guid tenantId, Guid userId, string name)
    {
        NoteId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}
