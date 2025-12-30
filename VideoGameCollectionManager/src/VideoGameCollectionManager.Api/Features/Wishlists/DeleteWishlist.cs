// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Wishlists;

public class DeleteWishlistCommand : IRequest<bool>
{
    public Guid WishlistId { get; set; }
}

public class DeleteWishlistCommandHandler : IRequestHandler<DeleteWishlistCommand, bool>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public DeleteWishlistCommandHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.WishlistId == request.WishlistId, cancellationToken);

        if (wishlist == null)
        {
            return false;
        }

        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
