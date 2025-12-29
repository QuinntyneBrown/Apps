using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class DeleteFavorite
{
    public record Command(Guid UserId, Guid FavoriteId) : IRequest<bool>;

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
            _logger.LogInformation("Deleting favorite {FavoriteId} for user {UserId}", request.FavoriteId, request.UserId);

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.FavoriteId == request.FavoriteId && f.UserId == request.UserId, cancellationToken);

            if (favorite == null)
            {
                _logger.LogWarning("Favorite {FavoriteId} not found for user {UserId}", request.FavoriteId, request.UserId);
                return false;
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Favorite {FavoriteId} deleted", request.FavoriteId);
            return true;
        }
    }
}
