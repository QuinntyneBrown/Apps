using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetReviews
{
    public record Query(
        Guid UserId,
        string? ContentType,
        bool? HasSpoilers,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResult<ReviewDto>>;

    public class Handler : IRequestHandler<Query, PaginatedResult<ReviewDto>>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedResult<ReviewDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting reviews for user {UserId}", request.UserId);

            var query = _context.Reviews
                .Include(r => r.Themes)
                .Where(r => r.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.ContentType) && Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                query = query.Where(r => r.ContentType == contentType);
            }

            if (request.HasSpoilers.HasValue)
            {
                query = query.Where(r => r.HasSpoilers == request.HasSpoilers.Value);
            }

            query = query.OrderByDescending(r => r.ReviewDate);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<ReviewDto>(
                items.Select(r => r.ToDto()).ToList(),
                totalCount,
                request.Page,
                request.PageSize
            );
        }
    }
}
