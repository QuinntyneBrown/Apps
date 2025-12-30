// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Wishlists;

public class CreateWishlistCommand : IRequest<WishlistDto>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Platform? Platform { get; set; }
    public Genre? Genre { get; set; }
    public int Priority { get; set; }
    public string? Notes { get; set; }
    public bool IsAcquired { get; set; }
}

public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistCommand, WishlistDto>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public CreateWishlistCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<WishlistDto> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Platform = request.Platform,
            Genre = request.Genre,
            Priority = request.Priority,
            Notes = request.Notes,
            IsAcquired = request.IsAcquired,
            CreatedAt = DateTime.UtcNow
        };

        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync(cancellationToken);

        return wishlist.ToDto();
    }
}
