// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;

namespace GroceryShoppingListApp.Api.Features.Stores;

public class CreateStoreCommand : IRequest<StoreDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
}

public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, StoreDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public CreateStoreCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<StoreDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = new Store
        {
            StoreId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow
        };

        _context.Stores.Add(store);
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
