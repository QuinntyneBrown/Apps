using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetRatings
{
    public record Query(
        Guid UserId,
        string? ContentType,
        decimal? MinRating,
        decimal? MaxRating,
        string? SortBy,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResult<RatingDto>>;

    public class Handler : IRequestHandler<Query, PaginatedResult<RatingDto>>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedResult<RatingDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting ratings for user {UserId}", request.UserId);

            var query = _context.Ratings.Where(r => r.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.ContentType) && Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                query = query.Where(r => r.ContentType == contentType);
            }

            if (request.MinRating.HasValue)
            {
                query = query.Where(r => r.RatingValue >= request.MinRating.Value);
            }

            if (request.MaxRating.HasValue)
            {
                query = query.Where(r => r.RatingValue <= request.MaxRating.Value);
            }

            query = request.SortBy?.ToLower() switch
            {
                "rating" => query.OrderByDescending(r => r.RatingValue),
                "date" => query.OrderByDescending(r => r.RatingDate),
                _ => query.OrderByDescending(r => r.RatingDate)
            };

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<RatingDto>(
                items.Select(r => r.ToDto()).ToList(),
                totalCount,
                request.Page,
                request.PageSize
            );
        }
    }
}
