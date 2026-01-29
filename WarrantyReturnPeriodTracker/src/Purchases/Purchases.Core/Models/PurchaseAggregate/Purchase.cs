namespace Purchases.Core.Models;

public class Purchase
{
    public Guid PurchaseId { get; set; }
    public Guid UserId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? StoreName { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
