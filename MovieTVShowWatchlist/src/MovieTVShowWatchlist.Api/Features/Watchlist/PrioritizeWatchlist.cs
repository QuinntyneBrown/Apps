using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class PrioritizeWatchlist
{
    public record Command(
        Guid UserId,
        Dictionary<Guid, int> ItemRankings,
        string? SortingCriteria,
        List<string>? MoodBasedCategories
    ) : IRequest<List<WatchlistItemDto>>;

    public class Handler : IRequestHandler<Command, List<WatchlistItemDto>>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<WatchlistItemDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Prioritizing watchlist for user {UserId}", request.UserId);

            var itemIds = request.ItemRankings.Keys.ToList();
            var items = await _context.WatchlistItems
                .Where(w => w.UserId == request.UserId && itemIds.Contains(w.WatchlistItemId))
                .ToListAsync(cancellationToken);

            foreach (var item in items)
            {
                if (request.ItemRankings.TryGetValue(item.WatchlistItemId, out var rank))
                {
                    item.PriorityRank = rank;
                    item.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Watchlist prioritized for user {UserId}", request.UserId);

            return items.OrderBy(i => i.PriorityRank).Select(i => i.ToDto()).ToList();
        }
    }
}
