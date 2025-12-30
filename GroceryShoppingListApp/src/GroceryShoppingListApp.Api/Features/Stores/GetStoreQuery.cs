// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.Stores;

public class GetStoreQuery : IRequest<StoreDto?>
{
    public Guid StoreId { get; set; }
}

public class GetStoreQueryHandler : IRequestHandler<GetStoreQuery, StoreDto?>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetStoreQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<StoreDto?> Handle(GetStoreQuery request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.StoreId == request.StoreId, cancellationToken);

        if (store == null)
            return null;

        return new StoreDto
        {
            StoreId = store.StoreId,
            UserId = store.UserId,
            Name = store.Name,
            Address = store.Address,
            CreatedAt = store.CreatedAt
        };
    }
}
