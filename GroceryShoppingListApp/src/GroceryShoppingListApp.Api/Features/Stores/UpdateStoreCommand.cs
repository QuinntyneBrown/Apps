// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.Stores;

public class UpdateStoreCommand : IRequest<StoreDto>
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
}

public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, StoreDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public UpdateStoreCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<StoreDto> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores
            .FirstOrDefaultAsync(x => x.StoreId == request.StoreId, cancellationToken)
            ?? throw new InvalidOperationException($"Store with ID {request.StoreId} not found");

        store.Name = request.Name;
        store.Address = request.Address;

        await _context.SaveChangesAsync(cancellationToken);

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
