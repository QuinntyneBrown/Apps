using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Books;

public record UpdateBookCommand : IRequest<BookDto?>
{
    public Guid BookId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string? ISBN { get; init; }
    public Genre Genre { get; init; }
    public ReadingStatus Status { get; init; }
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<UpdateBookCommandHandler> _logger;

    public UpdateBookCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<UpdateBookCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookDto?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating book {BookId}", request.BookId);

        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.BookId == request.BookId, cancellationToken);

        if (book == null)
        {
            _logger.LogWarning("Book {BookId} not found", request.BookId);
            return null;
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.ISBN = request.ISBN;
        book.Genre = request.Genre;
        book.Status = request.Status;
        book.TotalPages = request.TotalPages;
        book.CurrentPage = request.CurrentPage;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated book {BookId}", request.BookId);

        return book.ToDto();
    }
}
