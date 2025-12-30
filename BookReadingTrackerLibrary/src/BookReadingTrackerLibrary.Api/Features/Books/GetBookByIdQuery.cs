using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Books;

public record GetBookByIdQuery : IRequest<BookDto?>
{
    public Guid BookId { get; init; }
}

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<GetBookByIdQueryHandler> _logger;

    public GetBookByIdQueryHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<GetBookByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting book {BookId}", request.BookId);

        var book = await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BookId == request.BookId, cancellationToken);

        if (book == null)
        {
            _logger.LogWarning("Book {BookId} not found", request.BookId);
            return null;
        }

        return book.ToDto();
    }
}
