using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.ReadingLogs;

public record CreateReadingLogCommand : IRequest<ReadingLogDto>
{
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    public int StartPage { get; init; }
    public int EndPage { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
}

public class CreateReadingLogCommandHandler : IRequestHandler<CreateReadingLogCommand, ReadingLogDto>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<CreateReadingLogCommandHandler> _logger;

    public CreateReadingLogCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<CreateReadingLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReadingLogDto> Handle(CreateReadingLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reading log for user {UserId}, book {BookId}",
            request.UserId,
            request.BookId);

        var readingLog = new ReadingLog
        {
            ReadingLogId = Guid.NewGuid(),
            UserId = request.UserId,
            BookId = request.BookId,
            StartPage = request.StartPage,
            EndPage = request.EndPage,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ReadingLogs.Add(readingLog);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reading log {ReadingLogId} for user {UserId}",
            readingLog.ReadingLogId,
            request.UserId);

        return readingLog.ToDto();
    }
}
