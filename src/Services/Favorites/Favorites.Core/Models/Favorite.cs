namespace Favorites.Core.Models;

public class Favorite
{
    public Guid FavoriteId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid PromptId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
