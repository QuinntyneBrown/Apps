using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetFavorites
{
    public record Query(
        Guid UserId,
        string? ContentType,
        string? Category,
        string? SortBy,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResult<FavoriteDto>>;

    public class Handler : IRequestHandler<Query, PaginatedResult<FavoriteDto>>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedResult<FavoriteDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting favorites for user {UserId}", request.UserId);

            var query = _context.Favorites.Where(f => f.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.ContentType) && Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                query = query.Where(f => f.ContentType == contentType);
            }

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(f => f.FavoriteCategory == request.Category);
            }

            query = request.SortBy?.ToLower() switch
            {
                "date" => query.OrderByDescending(f => f.AddedDate),
                "rewatch" => query.OrderByDescending(f => f.RewatchCount),
                _ => query.OrderByDescending(f => f.AddedDate)
            };

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<FavoriteDto>(
                items.Select(f => f.ToDto()).ToList(),
                totalCount,
                request.Page,
                request.PageSize
            );
        }
    }
}
