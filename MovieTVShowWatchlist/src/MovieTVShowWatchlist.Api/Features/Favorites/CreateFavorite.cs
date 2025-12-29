using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class CreateFavorite
{
    public record Command(
        Guid UserId,
        Guid ContentId,
        string ContentType,
        string? FavoriteCategory,
        string? EmotionalSignificance
    ) : IRequest<FavoriteDto>;

    public class Handler : IRequestHandler<Command, FavoriteDto>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<FavoriteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating favorite for content {ContentId} by user {UserId}", request.ContentId, request.UserId);

            if (!Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                throw new ArgumentException($"Invalid content type: {request.ContentType}");
            }

            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.ContentId == request.ContentId && f.ContentType == contentType, cancellationToken);

            if (existing != null)
            {
                throw new InvalidOperationException("Content is already in favorites");
            }

            var favorite = new Favorite
            {
                FavoriteId = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.ContentId,
                ContentType = contentType,
                AddedDate = DateTime.UtcNow,
                FavoriteCategory = request.FavoriteCategory,
                EmotionalSignificance = request.EmotionalSignificance,
                RewatchCount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Favorite {FavoriteId} created for content {ContentId}", favorite.FavoriteId, request.ContentId);

            return favorite.ToDto();
        }
    }
}
