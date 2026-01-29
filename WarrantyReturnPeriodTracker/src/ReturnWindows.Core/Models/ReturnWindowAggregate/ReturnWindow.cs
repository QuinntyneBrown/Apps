namespace ReturnWindows.Core.Models;

public class ReturnWindow
{
    public Guid ReturnWindowId { get; set; }
    public Guid PurchaseId { get; set; }
    public Guid UserId { get; set; }
    public int ReturnDays { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? StoreName { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
