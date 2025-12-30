using BookReadingTrackerLibrary.Core;

namespace BookReadingTrackerLibrary.Api.Features.Wishlists;

public record WishlistDto
{
    public Guid WishlistId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string? ISBN { get; init; }
    public Genre? Genre { get; init; }
    public int Priority { get; init; }
    public string? Notes { get; init; }
    public bool IsAcquired { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class WishlistExtensions
{
    public static WishlistDto ToDto(this Wishlist wishlist)
    {
        return new WishlistDto
        {
            WishlistId = wishlist.WishlistId,
            UserId = wishlist.UserId,
            Title = wishlist.Title,
            Author = wishlist.Author,
            ISBN = wishlist.ISBN,
            Genre = wishlist.Genre,
            Priority = wishlist.Priority,
            Notes = wishlist.Notes,
            IsAcquired = wishlist.IsAcquired,
            CreatedAt = wishlist.CreatedAt,
        };
    }
}
