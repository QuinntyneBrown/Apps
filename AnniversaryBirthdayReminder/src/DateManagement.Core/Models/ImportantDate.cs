namespace DateManagement.Core.Models;

public class ImportantDate
{
    public Guid ImportantDateId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public int DaysBeforeReminder { get; set; } = 7;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
