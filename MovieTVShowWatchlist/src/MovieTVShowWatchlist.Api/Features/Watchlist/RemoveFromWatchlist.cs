using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class RemoveFromWatchlist
{
    public record Command(
        Guid UserId,
        Guid ItemId,
        string? RemovalReason,
        string? AlternativeAdded
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Removing item {ItemId} from watchlist for user {UserId}", request.ItemId, request.UserId);

            var item = await _context.WatchlistItems
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(w => w.WatchlistItemId == request.ItemId && w.UserId == request.UserId, cancellationToken);

            if (item == null)
            {
                _logger.LogWarning("Watchlist item {ItemId} not found for user {UserId}", request.ItemId, request.UserId);
                return false;
            }

            RemovalReason? reason = null;
            if (!string.IsNullOrEmpty(request.RemovalReason) && Enum.TryParse<RemovalReason>(request.RemovalReason, true, out var parsed))
            {
                reason = parsed;
            }

            item.IsDeleted = true;
            item.DeletedAt = DateTime.UtcNow;
            item.RemovalReason = reason;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Item {ItemId} removed from watchlist", request.ItemId);
            return true;
        }
    }
}
