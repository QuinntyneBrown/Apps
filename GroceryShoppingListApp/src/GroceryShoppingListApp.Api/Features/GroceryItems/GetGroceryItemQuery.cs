// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryItems;

public class GetGroceryItemQuery : IRequest<GroceryItemDto?>
{
    public Guid GroceryItemId { get; set; }
}

public class GetGroceryItemQueryHandler : IRequestHandler<GetGroceryItemQuery, GroceryItemDto?>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetGroceryItemQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<GroceryItemDto?> Handle(GetGroceryItemQuery request, CancellationToken cancellationToken)
    {
        var groceryItem = await _context.GroceryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.GroceryItemId == request.GroceryItemId, cancellationToken);

        if (groceryItem == null)
            return null;

        return new GroceryItemDto
        {
            GroceryItemId = groceryItem.GroceryItemId,
            GroceryListId = groceryItem.GroceryListId,
            Name = groceryItem.Name,
            Category = groceryItem.Category,
            Quantity = groceryItem.Quantity,
            IsChecked = groceryItem.IsChecked,
            CreatedAt = groceryItem.CreatedAt
        };
    }
}
