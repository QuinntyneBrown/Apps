using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class AddTVShowToWatchlist
{
    public record Command(
        Guid UserId,
        Guid ShowId,
        string Title,
        int PremiereYear,
        List<string>? Genres,
        int? NumberOfSeasons,
        string? Status,
        string? Priority,
        string? StreamingPlatform
    ) : IRequest<WatchlistItemDto>;

    public class Handler : IRequestHandler<Command, WatchlistItemDto>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<WatchlistItemDto> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding TV show {ShowId} to watchlist for user {UserId}", request.ShowId, request.UserId);

            var existingItem = await _context.WatchlistItems
                .FirstOrDefaultAsync(w => w.UserId == request.UserId && w.ContentId == request.ShowId && !w.IsDeleted, cancellationToken);

            if (existingItem != null)
            {
                _logger.LogWarning("TV show {ShowId} already exists in watchlist for user {UserId}", request.ShowId, request.UserId);
                throw new InvalidOperationException("TV show already exists in watchlist");
            }

            var maxRank = await _context.WatchlistItems
                .Where(w => w.UserId == request.UserId && !w.IsDeleted)
                .MaxAsync(w => (int?)w.PriorityRank, cancellationToken) ?? 0;

            PriorityLevel? priorityLevel = null;
            if (!string.IsNullOrEmpty(request.Priority) && Enum.TryParse<PriorityLevel>(request.Priority, true, out var parsed))
            {
                priorityLevel = parsed;
            }

            var watchlistItem = new WatchlistItem
            {
                WatchlistItemId = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.ShowId,
                ContentType = ContentType.TVShow,
                Title = request.Title,
                AddedDate = DateTime.UtcNow,
                PriorityLevel = priorityLevel,
                PriorityRank = maxRank + 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.WatchlistItems.Add(watchlistItem);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("TV show {ShowId} added to watchlist with ID {WatchlistItemId}", request.ShowId, watchlistItem.WatchlistItemId);

            return watchlistItem.ToDto();
        }
    }
}
