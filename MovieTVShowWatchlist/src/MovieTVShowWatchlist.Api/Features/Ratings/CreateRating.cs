using MediatR;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class CreateRating
{
    public record Command(
        Guid UserId,
        Guid ContentId,
        string ContentType,
        decimal RatingValue,
        string RatingScale,
        DateTime RatingDate,
        DateTime? ViewingDate,
        bool IsRewatchRating,
        string? Mood
    ) : IRequest<RatingDto>;

    public class Handler : IRequestHandler<Command, RatingDto>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RatingDto> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating rating for content {ContentId} by user {UserId}", request.ContentId, request.UserId);

            if (!Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                throw new ArgumentException($"Invalid content type: {request.ContentType}");
            }

            if (!Enum.TryParse<RatingScale>(request.RatingScale, true, out var ratingScale))
            {
                throw new ArgumentException($"Invalid rating scale: {request.RatingScale}");
            }

            var rating = new Rating
            {
                RatingId = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.ContentId,
                ContentType = contentType,
                RatingValue = request.RatingValue,
                RatingScale = ratingScale,
                RatingDate = request.RatingDate,
                ViewingDate = request.ViewingDate,
                IsRewatchRating = request.IsRewatchRating,
                Mood = request.Mood,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Rating {RatingId} created for content {ContentId}", rating.RatingId, request.ContentId);

            return rating.ToDto();
        }
    }
}
