namespace ReadingLogs.Core.Models;

public class ReadingLog
{
    public Guid ReadingLogId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public int PagesRead { get; private set; }
    public int StartPage { get; private set; }
    public int EndPage { get; private set; }
    public int ReadingMinutes { get; private set; }
    public DateTime ReadAt { get; private set; }
    public string? Notes { get; private set; }

    private ReadingLog() { }

    public ReadingLog(Guid tenantId, Guid userId, Guid bookId, int startPage, int endPage, int readingMinutes, DateTime readAt, string? notes = null)
    {
        if (startPage < 0)
            throw new ArgumentException("Start page cannot be negative.", nameof(startPage));
        if (endPage < startPage)
            throw new ArgumentException("End page must be greater than or equal to start page.", nameof(endPage));
        if (readingMinutes <= 0)
            throw new ArgumentException("Reading minutes must be positive.", nameof(readingMinutes));

        ReadingLogId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BookId = bookId;
        StartPage = startPage;
        EndPage = endPage;
        PagesRead = endPage - startPage;
        ReadingMinutes = readingMinutes;
        ReadAt = readAt;
        Notes = notes;
    }

    public void Update(int? startPage = null, int? endPage = null, int? readingMinutes = null, string? notes = null)
    {
        if (startPage.HasValue || endPage.HasValue)
        {
            var newStart = startPage ?? StartPage;
            var newEnd = endPage ?? EndPage;

            if (newStart < 0)
                throw new ArgumentException("Start page cannot be negative.", nameof(startPage));
            if (newEnd < newStart)
                throw new ArgumentException("End page must be greater than or equal to start page.", nameof(endPage));

            StartPage = newStart;
            EndPage = newEnd;
            PagesRead = newEnd - newStart;
        }

        if (readingMinutes.HasValue)
        {
            if (readingMinutes.Value <= 0)
                throw new ArgumentException("Reading minutes must be positive.", nameof(readingMinutes));
            ReadingMinutes = readingMinutes.Value;
        }

        if (notes != null)
            Notes = notes;
    }
}
