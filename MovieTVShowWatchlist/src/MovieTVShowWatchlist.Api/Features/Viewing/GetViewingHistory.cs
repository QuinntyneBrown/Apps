using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class GetViewingHistory
{
    public record Query(
        Guid UserId,
        DateTime? StartDate,
        DateTime? EndDate,
        string? ContentType,
        string? Platform,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResult<ViewingRecordDto>>;

    public class Handler : IRequestHandler<Query, PaginatedResult<ViewingRecordDto>>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedResult<ViewingRecordDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting viewing history for user {UserId}", request.UserId);

            var query = _context.ViewingRecords
                .Include(v => v.Companions)
                .Where(v => v.UserId == request.UserId);

            if (request.StartDate.HasValue)
            {
                query = query.Where(v => v.WatchDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(v => v.WatchDate <= request.EndDate.Value);
            }

            if (!string.IsNullOrEmpty(request.ContentType) && Enum.TryParse<ContentType>(request.ContentType, true, out var contentType))
            {
                query = query.Where(v => v.ContentType == contentType);
            }

            if (!string.IsNullOrEmpty(request.Platform))
            {
                query = query.Where(v => v.ViewingPlatform == request.Platform);
            }

            query = query.OrderByDescending(v => v.WatchDate);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<ViewingRecordDto>(
                items.Select(i => i.ToDto()).ToList(),
                totalCount,
                request.Page,
                request.PageSize
            );
        }
    }
}
