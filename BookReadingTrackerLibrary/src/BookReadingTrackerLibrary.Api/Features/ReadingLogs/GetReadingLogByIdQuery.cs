using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.ReadingLogs;

public record GetReadingLogByIdQuery : IRequest<ReadingLogDto?>
{
    public Guid ReadingLogId { get; init; }
}

public class GetReadingLogByIdQueryHandler : IRequestHandler<GetReadingLogByIdQuery, ReadingLogDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<GetReadingLogByIdQueryHandler> _logger;

    public GetReadingLogByIdQueryHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<GetReadingLogByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReadingLogDto?> Handle(GetReadingLogByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reading log {ReadingLogId}", request.ReadingLogId);

        var readingLog = await _context.ReadingLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReadingLogId == request.ReadingLogId, cancellationToken);

        if (readingLog == null)
        {
            _logger.LogWarning("Reading log {ReadingLogId} not found", request.ReadingLogId);
            return null;
        }

        return readingLog.ToDto();
    }
}
