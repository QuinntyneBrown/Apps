using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetWatchlist
{
    public record Query(
        Guid UserId,
        string? SortBy,
        string? FilterByGenre,
        string? FilterByPriority,
        string? FilterByMood,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResult<WatchlistItemDto>>;

    public class Handler : IRequestHandler<Query, PaginatedResult<WatchlistItemDto>>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedResult<WatchlistItemDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting watchlist for user {UserId}", request.UserId);

            var query = _context.WatchlistItems
                .Where(w => w.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.FilterByPriority) && Enum.TryParse<PriorityLevel>(request.FilterByPriority, true, out var priority))
            {
                query = query.Where(w => w.PriorityLevel == priority);
            }

            if (!string.IsNullOrEmpty(request.FilterByMood))
            {
                query = query.Where(w => w.MoodCategory == request.FilterByMood);
            }

            query = request.SortBy?.ToLower() switch
            {
                "priority" => query.OrderBy(w => w.PriorityRank),
                "date" => query.OrderByDescending(w => w.AddedDate),
                "title" => query.OrderBy(w => w.Title),
                _ => query.OrderBy(w => w.PriorityRank)
            };

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<WatchlistItemDto>(
                items.Select(i => i.ToDto()).ToList(),
                totalCount,
                request.Page,
                request.PageSize
            );
        }
    }
}

public record PaginatedResult<T>(List<T> Items, int TotalCount, int Page, int PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}
