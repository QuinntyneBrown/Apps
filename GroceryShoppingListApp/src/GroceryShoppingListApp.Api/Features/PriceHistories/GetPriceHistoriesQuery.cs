// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.PriceHistories;

public class GetPriceHistoriesQuery : IRequest<List<PriceHistoryDto>>
{
    public Guid? GroceryItemId { get; set; }
    public Guid? StoreId { get; set; }
}

public class GetPriceHistoriesQueryHandler : IRequestHandler<GetPriceHistoriesQuery, List<PriceHistoryDto>>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetPriceHistoriesQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<List<PriceHistoryDto>> Handle(GetPriceHistoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.PriceHistories.AsNoTracking();

        if (request.GroceryItemId.HasValue)
        {
            query = query.Where(x => x.GroceryItemId == request.GroceryItemId.Value);
        }

        if (request.StoreId.HasValue)
        {
            query = query.Where(x => x.StoreId == request.StoreId.Value);
        }

        var priceHistories = await query.ToListAsync(cancellationToken);

        return priceHistories.Select(ph => new PriceHistoryDto
        {
            PriceHistoryId = ph.PriceHistoryId,
            GroceryItemId = ph.GroceryItemId,
            StoreId = ph.StoreId,
            Price = ph.Price,
            Date = ph.Date,
            CreatedAt = ph.CreatedAt
        }).ToList();
    }
}
