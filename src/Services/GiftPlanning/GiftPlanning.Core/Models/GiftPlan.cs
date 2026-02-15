namespace GiftPlanning.Core.Models;

public class GiftPlan
{
    public Guid GiftPlanId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid? CelebrationId { get; set; }
    public string RecipientName { get; set; } = string.Empty;
    public string GiftIdea { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public string? Notes { get; set; }
    public bool IsPurchased { get; set; } = false;
    public DateTime? PurchaseDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
