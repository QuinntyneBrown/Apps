using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.ReadingLogs;

public record UpdateReadingLogCommand : IRequest<ReadingLogDto?>
{
    public Guid ReadingLogId { get; init; }
    public int StartPage { get; init; }
    public int EndPage { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
}

public class UpdateReadingLogCommandHandler : IRequestHandler<UpdateReadingLogCommand, ReadingLogDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<UpdateReadingLogCommandHandler> _logger;

    public UpdateReadingLogCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<UpdateReadingLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReadingLogDto?> Handle(UpdateReadingLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reading log {ReadingLogId}", request.ReadingLogId);

        var readingLog = await _context.ReadingLogs
            .FirstOrDefaultAsync(r => r.ReadingLogId == request.ReadingLogId, cancellationToken);

        if (readingLog == null)
        {
            _logger.LogWarning("Reading log {ReadingLogId} not found", request.ReadingLogId);
            return null;
        }

        readingLog.StartPage = request.StartPage;
        readingLog.EndPage = request.EndPage;
        readingLog.StartTime = request.StartTime;
        readingLog.EndTime = request.EndTime;
        readingLog.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated reading log {ReadingLogId}", request.ReadingLogId);

        return readingLog.ToDto();
    }
}
