// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Features.Wishlists;

public class GetAllWishlistsQuery : IRequest<List<WishlistDto>>
{
}

public class GetAllWishlistsQueryHandler : IRequestHandler<GetAllWishlistsQuery, List<WishlistDto>>
{
    private readonly IVideoGameCollectionManagerContext _context;

    public GetAllWishlistsQueryHandler(IVideoGameCollectionManagerContext context)
    {
        _context = context;
    }

    public async Task<List<WishlistDto>> Handle(GetAllWishlistsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Wishlists
            .Select(w => w.ToDto())
            .ToListAsync(cancellationToken);
    }
}
