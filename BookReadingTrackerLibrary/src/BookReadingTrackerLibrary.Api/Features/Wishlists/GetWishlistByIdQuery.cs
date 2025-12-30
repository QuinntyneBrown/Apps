using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Wishlists;

public record GetWishlistByIdQuery : IRequest<WishlistDto?>
{
    public Guid WishlistId { get; init; }
}

public class GetWishlistByIdQueryHandler : IRequestHandler<GetWishlistByIdQuery, WishlistDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<GetWishlistByIdQueryHandler> _logger;

    public GetWishlistByIdQueryHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<GetWishlistByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WishlistDto?> Handle(GetWishlistByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wishlist item {WishlistId}", request.WishlistId);

        var wishlist = await _context.Wishlists
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.WishlistId == request.WishlistId, cancellationToken);

        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist item {WishlistId} not found", request.WishlistId);
            return null;
        }

        return wishlist.ToDto();
    }
}
