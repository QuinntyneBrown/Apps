using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Books;

public record DeleteBookCommand : IRequest<bool>
{
    public Guid BookId { get; init; }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<DeleteBookCommandHandler> _logger;

    public DeleteBookCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<DeleteBookCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting book {BookId}", request.BookId);

        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.BookId == request.BookId, cancellationToken);

        if (book == null)
        {
            _logger.LogWarning("Book {BookId} not found", request.BookId);
            return false;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted book {BookId}", request.BookId);

        return true;
    }
}
