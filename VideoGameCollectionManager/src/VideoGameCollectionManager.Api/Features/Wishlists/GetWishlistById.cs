// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Wishlists;

public class GetWishlistByIdQuery : IRequest<WishlistDto?>
{
    public Guid WishlistId { get; set; }
}

public class GetWishlistByIdQueryHandler : IRequestHandler<GetWishlistByIdQuery, WishlistDto?>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public GetWishlistByIdQueryHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<WishlistDto?> Handle(GetWishlistByIdQuery request, CancellationToken cancellationToken)
    {
        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.WishlistId == request.WishlistId, cancellationToken);

        return wishlist?.ToDto();
    }
}
