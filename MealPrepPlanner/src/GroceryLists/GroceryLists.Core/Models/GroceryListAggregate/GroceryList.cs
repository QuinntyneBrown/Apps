namespace GroceryLists.Core.Models;

public class GroceryList
{
    public Guid GroceryListId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Items { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? ShoppingDate { get; set; }
    public string? Store { get; set; }
    public decimal? EstimatedCost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
