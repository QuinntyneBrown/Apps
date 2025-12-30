// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.PriceHistories;

public class UpdatePriceHistoryCommand : IRequest<PriceHistoryDto>
{
    public Guid PriceHistoryId { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
}

public class UpdatePriceHistoryCommandHandler : IRequestHandler<UpdatePriceHistoryCommand, PriceHistoryDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public UpdatePriceHistoryCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<PriceHistoryDto> Handle(UpdatePriceHistoryCommand request, CancellationToken cancellationToken)
    {
        var priceHistory = await _context.PriceHistories
            .FirstOrDefaultAsync(x => x.PriceHistoryId == request.PriceHistoryId, cancellationToken)
            ?? throw new InvalidOperationException($"PriceHistory with ID {request.PriceHistoryId} not found");

        priceHistory.Price = request.Price;
        priceHistory.Date = request.Date;

        await _context.SaveChangesAsync(cancellationToken);

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
