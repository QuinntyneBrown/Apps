namespace Games.Core.Models;

public class Game
{
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Condition { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
