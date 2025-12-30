using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Wishlists;

public record GetWishlistsQuery : IRequest<IEnumerable<WishlistDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsAcquired { get; init; }
}

public class GetWishlistsQueryHandler : IRequestHandler<GetWishlistsQuery, IEnumerable<WishlistDto>>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<GetWishlistsQueryHandler> _logger;

    public GetWishlistsQueryHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<GetWishlistsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WishlistDto>> Handle(GetWishlistsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wishlist items for user {UserId}", request.UserId);

        var query = _context.Wishlists.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(w => w.UserId == request.UserId.Value);
        }

        if (request.IsAcquired.HasValue)
        {
            query = query.Where(w => w.IsAcquired == request.IsAcquired.Value);
        }

        var wishlists = await query
            .OrderByDescending(w => w.Priority)
            .ThenByDescending(w => w.CreatedAt)
            .ToListAsync(cancellationToken);

        return wishlists.Select(w => w.ToDto());
    }
}
