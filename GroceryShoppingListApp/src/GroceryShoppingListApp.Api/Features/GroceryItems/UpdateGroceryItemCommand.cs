// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryItems;

public class UpdateGroceryItemCommand : IRequest<GroceryItemDto>
{
    public Guid GroceryItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
    public int Quantity { get; set; }
    public bool IsChecked { get; set; }
}

public class UpdateGroceryItemCommandHandler : IRequestHandler<UpdateGroceryItemCommand, GroceryItemDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public UpdateGroceryItemCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<GroceryItemDto> Handle(UpdateGroceryItemCommand request, CancellationToken cancellationToken)
    {
        var groceryItem = await _context.GroceryItems
            .FirstOrDefaultAsync(x => x.GroceryItemId == request.GroceryItemId, cancellationToken)
            ?? throw new InvalidOperationException($"GroceryItem with ID {request.GroceryItemId} not found");

        groceryItem.Name = request.Name;
        groceryItem.Category = request.Category;
        groceryItem.Quantity = request.Quantity;
        groceryItem.IsChecked = request.IsChecked;

        await _context.SaveChangesAsync(cancellationToken);

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
