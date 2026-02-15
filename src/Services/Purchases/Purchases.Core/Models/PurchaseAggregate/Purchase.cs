namespace Purchases.Core.Models;

public class Purchase
{
    public Guid PurchaseId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid IdeaId { get; set; }
    public decimal Amount { get; set; }
    public string? Store { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string? Notes { get; set; }
    public bool IsWrapped { get; set; }
    public bool IsDelivered { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
