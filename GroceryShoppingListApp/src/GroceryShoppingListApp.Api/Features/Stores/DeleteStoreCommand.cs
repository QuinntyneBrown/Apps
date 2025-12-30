// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.Stores;

public class DeleteStoreCommand : IRequest<Unit>
{
    public Guid StoreId { get; set; }
}

public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, Unit>
{
    private readonly IGroceryShoppingListAppContext _context;

    public DeleteStoreCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores
            .FirstOrDefaultAsync(x => x.StoreId == request.StoreId, cancellationToken)
            ?? throw new InvalidOperationException($"Store with ID {request.StoreId} not found");

        _context.Stores.Remove(store);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
