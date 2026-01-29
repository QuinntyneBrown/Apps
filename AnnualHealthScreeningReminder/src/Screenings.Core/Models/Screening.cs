namespace Screenings.Core.Models;

public class Screening
{
    public Guid ScreeningId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string ScreeningType { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? Result { get; set; }
    public string? Notes { get; set; }
    public int FrequencyMonths { get; set; } = 12;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
