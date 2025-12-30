using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.ReadingLogs;

public record DeleteReadingLogCommand : IRequest<bool>
{
    public Guid ReadingLogId { get; init; }
}

public class DeleteReadingLogCommandHandler : IRequestHandler<DeleteReadingLogCommand, bool>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<DeleteReadingLogCommandHandler> _logger;

    public DeleteReadingLogCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<DeleteReadingLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReadingLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reading log {ReadingLogId}", request.ReadingLogId);

        var readingLog = await _context.ReadingLogs
            .FirstOrDefaultAsync(r => r.ReadingLogId == request.ReadingLogId, cancellationToken);

        if (readingLog == null)
        {
            _logger.LogWarning("Reading log {ReadingLogId} not found", request.ReadingLogId);
            return false;
        }

        _context.ReadingLogs.Remove(readingLog);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reading log {ReadingLogId}", request.ReadingLogId);

        return true;
    }
}
