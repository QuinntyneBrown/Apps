using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Wishlists;

public record CreateWishlistCommand : IRequest<WishlistDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string? ISBN { get; init; }
    public Genre? Genre { get; init; }
    public int Priority { get; init; } = 3;
    public string? Notes { get; init; }
}

public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistCommand, WishlistDto>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<CreateWishlistCommandHandler> _logger;

    public CreateWishlistCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<CreateWishlistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WishlistDto> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating wishlist item for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            Genre = request.Genre,
            Priority = request.Priority,
            Notes = request.Notes,
            IsAcquired = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created wishlist item {WishlistId} for user {UserId}",
            wishlist.WishlistId,
            request.UserId);

        return wishlist.ToDto();
    }
}
