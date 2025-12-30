// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.PriceHistories;

public class GetPriceHistoryQuery : IRequest<PriceHistoryDto?>
{
    public Guid PriceHistoryId { get; set; }
}

public class GetPriceHistoryQueryHandler : IRequestHandler<GetPriceHistoryQuery, PriceHistoryDto?>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetPriceHistoryQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<PriceHistoryDto?> Handle(GetPriceHistoryQuery request, CancellationToken cancellationToken)
    {
        var priceHistory = await _context.PriceHistories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PriceHistoryId == request.PriceHistoryId, cancellationToken);

        if (priceHistory == null)
            return null;

        return new PriceHistoryDto
        {
            PriceHistoryId = priceHistory.PriceHistoryId,
            GroceryItemId = priceHistory.GroceryItemId,
            StoreId = priceHistory.StoreId,
            Price = priceHistory.Price,
            Date = priceHistory.Date,
            CreatedAt = priceHistory.CreatedAt
        };
    }
}
