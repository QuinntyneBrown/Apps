using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetShowProgress
{
    public record Query(Guid UserId, Guid ShowId) : IRequest<ShowProgressDto?>;

    public class Handler : IRequestHandler<Query, ShowProgressDto?>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ShowProgressDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting show progress for show {ShowId} and user {UserId}", request.ShowId, request.UserId);

            var progress = await _context.ShowProgresses
                .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.TVShowId == request.ShowId, cancellationToken);

            return progress?.ToDto();
        }
    }
}
