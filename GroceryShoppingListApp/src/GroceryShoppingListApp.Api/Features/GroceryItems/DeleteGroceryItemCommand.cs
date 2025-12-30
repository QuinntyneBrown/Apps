// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryItems;

public class DeleteGroceryItemCommand : IRequest<Unit>
{
    public Guid GroceryItemId { get; set; }
}

public class DeleteGroceryItemCommandHandler : IRequestHandler<DeleteGroceryItemCommand, Unit>
{
    private readonly IGroceryShoppingListAppContext _context;

    public DeleteGroceryItemCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGroceryItemCommand request, CancellationToken cancellationToken)
    {
        var groceryItem = await _context.GroceryItems
            .FirstOrDefaultAsync(x => x.GroceryItemId == request.GroceryItemId, cancellationToken)
            ?? throw new InvalidOperationException($"GroceryItem with ID {request.GroceryItemId} not found");

        _context.GroceryItems.Remove(groceryItem);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
