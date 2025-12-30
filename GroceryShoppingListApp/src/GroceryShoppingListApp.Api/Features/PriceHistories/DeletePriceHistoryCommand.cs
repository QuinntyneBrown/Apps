// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.PriceHistories;

public class DeletePriceHistoryCommand : IRequest<Unit>
{
    public Guid PriceHistoryId { get; set; }
}

public class DeletePriceHistoryCommandHandler : IRequestHandler<DeletePriceHistoryCommand, Unit>
{
    private readonly IGroceryShoppingListAppContext _context;

    public DeletePriceHistoryCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePriceHistoryCommand request, CancellationToken cancellationToken)
    {
        var priceHistory = await _context.PriceHistories
            .FirstOrDefaultAsync(x => x.PriceHistoryId == request.PriceHistoryId, cancellationToken)
            ?? throw new InvalidOperationException($"PriceHistory with ID {request.PriceHistoryId} not found");

        _context.PriceHistories.Remove(priceHistory);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
