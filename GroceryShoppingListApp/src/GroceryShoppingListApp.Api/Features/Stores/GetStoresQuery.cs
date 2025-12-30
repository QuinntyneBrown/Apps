// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.Stores;

public class GetStoresQuery : IRequest<List<StoreDto>>
{
    public Guid? UserId { get; set; }
}

public class GetStoresQueryHandler : IRequestHandler<GetStoresQuery, List<StoreDto>>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetStoresQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<List<StoreDto>> Handle(GetStoresQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Stores.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        var stores = await query.ToListAsync(cancellationToken);

        return stores.Select(s => new StoreDto
        {
            StoreId = s.StoreId,
            UserId = s.UserId,
            Name = s.Name,
            Address = s.Address,
            CreatedAt = s.CreatedAt
        }).ToList();
    }
}
