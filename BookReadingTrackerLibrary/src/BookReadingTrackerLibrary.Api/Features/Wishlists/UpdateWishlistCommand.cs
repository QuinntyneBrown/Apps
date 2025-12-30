using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Wishlists;

public record UpdateWishlistCommand : IRequest<WishlistDto?>
{
    public Guid WishlistId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string? ISBN { get; init; }
    public Genre? Genre { get; init; }
    public int Priority { get; init; }
    public string? Notes { get; init; }
    public bool IsAcquired { get; init; }
}

public class UpdateWishlistCommandHandler : IRequestHandler<UpdateWishlistCommand, WishlistDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<UpdateWishlistCommandHandler> _logger;

    public UpdateWishlistCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<UpdateWishlistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WishlistDto?> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating wishlist item {WishlistId}", request.WishlistId);

        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.WishlistId == request.WishlistId, cancellationToken);

        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist item {WishlistId} not found", request.WishlistId);
            return null;
        }

        wishlist.Title = request.Title;
        wishlist.Author = request.Author;
        wishlist.ISBN = request.ISBN;
        wishlist.Genre = request.Genre;
        wishlist.Priority = request.Priority;
        wishlist.Notes = request.Notes;
        wishlist.IsAcquired = request.IsAcquired;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated wishlist item {WishlistId}", request.WishlistId);

        return wishlist.ToDto();
    }
}
