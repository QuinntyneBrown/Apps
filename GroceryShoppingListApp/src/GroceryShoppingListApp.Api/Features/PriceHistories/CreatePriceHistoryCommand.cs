// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;

namespace GroceryShoppingListApp.Api.Features.PriceHistories;

public class CreatePriceHistoryCommand : IRequest<PriceHistoryDto>
{
    public Guid GroceryItemId { get; set; }
    public Guid StoreId { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
}

public class CreatePriceHistoryCommandHandler : IRequestHandler<CreatePriceHistoryCommand, PriceHistoryDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public CreatePriceHistoryCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<PriceHistoryDto> Handle(CreatePriceHistoryCommand request, CancellationToken cancellationToken)
    {
        var priceHistory = new PriceHistory
        {
            PriceHistoryId = Guid.NewGuid(),
            GroceryItemId = request.GroceryItemId,
            StoreId = request.StoreId,
            Price = request.Price,
            Date = request.Date,
            CreatedAt = DateTime.UtcNow
        };

        _context.PriceHistories.Add(priceHistory);
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
