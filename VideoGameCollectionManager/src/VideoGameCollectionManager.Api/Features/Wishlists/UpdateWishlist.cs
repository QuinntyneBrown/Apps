// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Wishlists;

public class UpdateWishlistCommand : IRequest<WishlistDto?>
{
    public Guid WishlistId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Platform? Platform { get; set; }
    public Genre? Genre { get; set; }
    public int Priority { get; set; }
    public string? Notes { get; set; }
    public bool IsAcquired { get; set; }
}

public class UpdateWishlistCommandHandler : IRequestHandler<UpdateWishlistCommand, WishlistDto?>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public UpdateWishlistCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<WishlistDto?> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.WishlistId == request.WishlistId, cancellationToken);

        if (wishlist == null)
        {
            return null;
        }

        wishlist.UserId = request.UserId;
        wishlist.Title = request.Title;
        wishlist.Platform = request.Platform;
        wishlist.Genre = request.Genre;
        wishlist.Priority = request.Priority;
        wishlist.Notes = request.Notes;
        wishlist.IsAcquired = request.IsAcquired;

        await _context.SaveChangesAsync(cancellationToken);

        return wishlist.ToDto();
    }
}
