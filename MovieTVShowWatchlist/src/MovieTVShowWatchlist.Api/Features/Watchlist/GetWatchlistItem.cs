using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetWatchlistItem
{
    public record Query(Guid UserId, Guid ItemId) : IRequest<WatchlistItemDto?>;

    public class Handler : IRequestHandler<Query, WatchlistItemDto?>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<WatchlistItemDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting watchlist item {ItemId} for user {UserId}", request.ItemId, request.UserId);

            var item = await _context.WatchlistItems
                .FirstOrDefaultAsync(w => w.WatchlistItemId == request.ItemId && w.UserId == request.UserId, cancellationToken);

            return item?.ToDto();
        }
    }
}
