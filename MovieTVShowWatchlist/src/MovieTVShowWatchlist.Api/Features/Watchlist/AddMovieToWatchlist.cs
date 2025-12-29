using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class AddMovieToWatchlist
{
    public record Command(
        Guid UserId,
        Guid MovieId,
        string Title,
        int ReleaseYear,
        List<string>? Genres,
        string? Director,
        int? Runtime,
        string? PriorityLevel,
        string? RecommendationSource,
        string? Availability
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
            _logger.LogInformation("Adding movie {MovieId} to watchlist for user {UserId}", request.MovieId, request.UserId);

            var existingItem = await _context.WatchlistItems
                .FirstOrDefaultAsync(w => w.UserId == request.UserId && w.ContentId == request.MovieId && !w.IsDeleted, cancellationToken);

            if (existingItem != null)
            {
                _logger.LogWarning("Movie {MovieId} already exists in watchlist for user {UserId}", request.MovieId, request.UserId);
                throw new InvalidOperationException("Movie already exists in watchlist");
            }

            var maxRank = await _context.WatchlistItems
                .Where(w => w.UserId == request.UserId && !w.IsDeleted)
                .MaxAsync(w => (int?)w.PriorityRank, cancellationToken) ?? 0;

            PriorityLevel? priorityLevel = null;
            if (!string.IsNullOrEmpty(request.PriorityLevel) && Enum.TryParse<PriorityLevel>(request.PriorityLevel, true, out var parsed))
            {
                priorityLevel = parsed;
            }

            var watchlistItem = new WatchlistItem
            {
                WatchlistItemId = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.MovieId,
                ContentType = ContentType.Movie,
                Title = request.Title,
                AddedDate = DateTime.UtcNow,
                PriorityLevel = priorityLevel,
                PriorityRank = maxRank + 1,
                RecommendationSource = request.RecommendationSource,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.WatchlistItems.Add(watchlistItem);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Movie {MovieId} added to watchlist with ID {WatchlistItemId}", request.MovieId, watchlistItem.WatchlistItemId);

            return watchlistItem.ToDto();
        }
    }
}
