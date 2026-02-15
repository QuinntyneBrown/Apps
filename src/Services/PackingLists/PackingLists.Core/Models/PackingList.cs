namespace PackingLists.Core.Models;

public class PackingList
{
    public Guid PackingListId { get; set; }
    public Guid TenantId { get; set; }
    public Guid TripId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public bool IsPacked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
