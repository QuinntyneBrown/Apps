using MediatR;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class MarkMovieWatched
{
    public record Command(
        Guid UserId,
        Guid MovieId,
        DateTime WatchDate,
        string? ViewingLocation,
        string? ViewingPlatform,
        List<string>? WatchedWith,
        string? ViewingContext,
        bool IsRewatch
    ) : IRequest<ViewingRecordDto>;

    public class Handler : IRequestHandler<Command, ViewingRecordDto>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ViewingRecordDto> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Marking movie {MovieId} as watched for user {UserId}", request.MovieId, request.UserId);

            ViewingContext? context = null;
            if (!string.IsNullOrEmpty(request.ViewingContext) && Enum.TryParse<ViewingContext>(request.ViewingContext, true, out var parsed))
            {
                context = parsed;
            }

            var record = new ViewingRecord
            {
                ViewingRecordId = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.MovieId,
                ContentType = ContentType.Movie,
                WatchDate = request.WatchDate,
                ViewingPlatform = request.ViewingPlatform,
                ViewingLocation = request.ViewingLocation,
                ViewingContext = context,
                IsRewatch = request.IsRewatch,
                CreatedAt = DateTime.UtcNow
            };

            if (request.WatchedWith != null)
            {
                foreach (var companion in request.WatchedWith)
                {
                    record.Companions.Add(new ViewingCompanion
                    {
                        ViewingCompanionId = Guid.NewGuid(),
                        CompanionName = companion,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            _context.ViewingRecords.Add(record);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Movie {MovieId} marked as watched with record ID {RecordId}", request.MovieId, record.ViewingRecordId);

            return record.ToDto();
        }
    }
}
