using BookReadingTrackerLibrary.Core;

namespace BookReadingTrackerLibrary.Api.Features.ReadingLogs;

public record ReadingLogDto
{
    public Guid ReadingLogId { get; init; }
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    public int StartPage { get; init; }
    public int EndPage { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PagesRead { get; init; }
    public int DurationInMinutes { get; init; }
}

public static class ReadingLogExtensions
{
    public static ReadingLogDto ToDto(this ReadingLog readingLog)
    {
        return new ReadingLogDto
        {
            ReadingLogId = readingLog.ReadingLogId,
            UserId = readingLog.UserId,
            BookId = readingLog.BookId,
            StartPage = readingLog.StartPage,
            EndPage = readingLog.EndPage,
            StartTime = readingLog.StartTime,
            EndTime = readingLog.EndTime,
            Notes = readingLog.Notes,
            CreatedAt = readingLog.CreatedAt,
            PagesRead = readingLog.GetPagesRead(),
            DurationInMinutes = readingLog.GetDurationInMinutes(),
        };
    }
}
