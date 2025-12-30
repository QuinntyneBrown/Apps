using BookReadingTrackerLibrary.Core;

namespace BookReadingTrackerLibrary.Api.Features.Books;

public record BookDto
{
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string? ISBN { get; init; }
    public Genre Genre { get; init; }
    public ReadingStatus Status { get; init; }
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? FinishDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public decimal ProgressPercentage { get; init; }
}

public static class BookExtensions
{
    public static BookDto ToDto(this Book book)
    {
        return new BookDto
        {
            BookId = book.BookId,
            UserId = book.UserId,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Genre = book.Genre,
            Status = book.Status,
            TotalPages = book.TotalPages,
            CurrentPage = book.CurrentPage,
            StartDate = book.StartDate,
            FinishDate = book.FinishDate,
            CreatedAt = book.CreatedAt,
            ProgressPercentage = book.GetProgressPercentage(),
        };
    }
}
