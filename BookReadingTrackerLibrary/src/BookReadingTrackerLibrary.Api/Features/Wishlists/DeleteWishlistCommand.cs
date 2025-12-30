using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Wishlists;

public record DeleteWishlistCommand : IRequest<bool>
{
    public Guid WishlistId { get; init; }
}

public class DeleteWishlistCommandHandler : IRequestHandler<DeleteWishlistCommand, bool>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<DeleteWishlistCommandHandler> _logger;

    public DeleteWishlistCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<DeleteWishlistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wishlist item {WishlistId}", request.WishlistId);

        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.WishlistId == request.WishlistId, cancellationToken);

        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist item {WishlistId} not found", request.WishlistId);
            return false;
        }

        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted wishlist item {WishlistId}", request.WishlistId);

        return true;
    }
}
