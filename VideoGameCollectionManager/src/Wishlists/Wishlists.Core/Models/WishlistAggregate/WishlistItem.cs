namespace Wishlists.Core.Models;

public class WishlistItem
{
    public Guid WishlistItemId { get; set; }
    public Guid UserId { get; set; }
    public string GameTitle { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public decimal? TargetPrice { get; set; }
    public int Priority { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
