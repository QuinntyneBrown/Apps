using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Books;

public record CreateBookCommand : IRequest<BookDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string? ISBN { get; init; }
    public Genre Genre { get; init; }
    public int TotalPages { get; init; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<CreateBookCommandHandler> _logger;

    public CreateBookCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<CreateBookCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating book for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var book = new Book
        {
            BookId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            Genre = request.Genre,
            Status = ReadingStatus.ToRead,
            TotalPages = request.TotalPages,
            CurrentPage = 0,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created book {BookId} for user {UserId}",
            book.BookId,
            request.UserId);

        return book.ToDto();
    }
}
