// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;

namespace GroceryShoppingListApp.Api.Features.GroceryItems;

public class CreateGroceryItemCommand : IRequest<GroceryItemDto>
{
    public Guid? GroceryListId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
    public int Quantity { get; set; }
}

public class CreateGroceryItemCommandHandler : IRequestHandler<CreateGroceryItemCommand, GroceryItemDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public CreateGroceryItemCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<GroceryItemDto> Handle(CreateGroceryItemCommand request, CancellationToken cancellationToken)
    {
        var groceryItem = new GroceryItem
        {
            GroceryItemId = Guid.NewGuid(),
            GroceryListId = request.GroceryListId,
            Name = request.Name,
            Category = request.Category,
            Quantity = request.Quantity,
            IsChecked = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.GroceryItems.Add(groceryItem);
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
