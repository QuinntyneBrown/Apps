using MediatR;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class CreateReview
{
    public record Command(
        Guid UserId,
        Guid ContentId,
        string ContentType,
        string ReviewText,
        bool HasSpoilers,
        List<string>? ThemesDiscussed,
        bool WouldRecommend,
        string? TargetAudience
    ) : IRequest<ReviewDto>;

    public class Handler : IRequestHandler<Command, ReviewDto>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ReviewDto> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating review for content {ContentId} by user {UserId}", request.ContentId, request.UserId);

            if (!Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                throw new ArgumentException($"Invalid content type: {request.ContentType}");
            }

            if (request.ReviewText.Length < 50)
            {
                throw new ArgumentException("Review text must be at least 50 characters");
            }

            if (request.ReviewText.Length > 10000)
            {
                throw new ArgumentException("Review text cannot exceed 10000 characters");
            }

            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                UserId = request.UserId,
                ContentId = request.ContentId,
                ContentType = contentType,
                ReviewText = request.ReviewText,
                HasSpoilers = request.HasSpoilers,
                ReviewDate = DateTime.UtcNow,
                WouldRecommend = request.WouldRecommend,
                TargetAudience = request.TargetAudience,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (request.ThemesDiscussed != null)
            {
                foreach (var theme in request.ThemesDiscussed)
                {
                    review.Themes.Add(new ReviewTheme
                    {
                        ReviewThemeId = Guid.NewGuid(),
                        Theme = theme,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Review {ReviewId} created for content {ContentId}", review.ReviewId, request.ContentId);

            return review.ToDto();
        }
    }
}
