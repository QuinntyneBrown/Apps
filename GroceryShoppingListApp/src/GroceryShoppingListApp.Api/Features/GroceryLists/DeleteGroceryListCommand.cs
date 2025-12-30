// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryLists;

public class DeleteGroceryListCommand : IRequest<Unit>
{
    public Guid GroceryListId { get; set; }
}

public class DeleteGroceryListCommandHandler : IRequestHandler<DeleteGroceryListCommand, Unit>
{
    private readonly IGroceryShoppingListAppContext _context;

    public DeleteGroceryListCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGroceryListCommand request, CancellationToken cancellationToken)
    {
        var groceryList = await _context.GroceryLists
            .FirstOrDefaultAsync(x => x.GroceryListId == request.GroceryListId, cancellationToken)
            ?? throw new InvalidOperationException($"GroceryList with ID {request.GroceryListId} not found");

        _context.GroceryLists.Remove(groceryList);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
