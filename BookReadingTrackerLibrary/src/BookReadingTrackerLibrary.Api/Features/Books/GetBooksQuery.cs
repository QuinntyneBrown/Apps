using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Books;

public record GetBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public Guid? UserId { get; init; }
    public Genre? Genre { get; init; }
    public ReadingStatus? Status { get; init; }
}

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<GetBooksQueryHandler> _logger;

    public GetBooksQueryHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<GetBooksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting books for user {UserId}", request.UserId);

        var query = _context.Books.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(b => b.UserId == request.UserId.Value);
        }

        if (request.Genre.HasValue)
        {
            query = query.Where(b => b.Genre == request.Genre.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(b => b.Status == request.Status.Value);
        }

        var books = await query
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);

        return books.Select(b => b.ToDto());
    }
}
