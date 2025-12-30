using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.ReadingLogs;

public record GetReadingLogsQuery : IRequest<IEnumerable<ReadingLogDto>>
{
    public Guid? UserId { get; init; }
    public Guid? BookId { get; init; }
}

public class GetReadingLogsQueryHandler : IRequestHandler<GetReadingLogsQuery, IEnumerable<ReadingLogDto>>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<GetReadingLogsQueryHandler> _logger;

    public GetReadingLogsQueryHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<GetReadingLogsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReadingLogDto>> Handle(GetReadingLogsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reading logs for user {UserId}", request.UserId);

        var query = _context.ReadingLogs.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.BookId.HasValue)
        {
            query = query.Where(r => r.BookId == request.BookId.Value);
        }

        var readingLogs = await query
            .OrderByDescending(r => r.StartTime)
            .ToListAsync(cancellationToken);

        return readingLogs.Select(r => r.ToDto());
    }
}
