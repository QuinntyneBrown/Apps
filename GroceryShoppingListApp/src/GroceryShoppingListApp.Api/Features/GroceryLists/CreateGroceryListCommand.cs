// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryLists;

public class CreateGroceryListCommand : IRequest<GroceryListDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateGroceryListCommandHandler : IRequestHandler<CreateGroceryListCommand, GroceryListDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public CreateGroceryListCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<GroceryListDto> Handle(CreateGroceryListCommand request, CancellationToken cancellationToken)
    {
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            CreatedDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.GroceryLists.Add(groceryList);
        await _context.SaveChangesAsync(cancellationToken);

        return new GroceryListDto
        {
            GroceryListId = groceryList.GroceryListId,
            UserId = groceryList.UserId,
            Name = groceryList.Name,
            CreatedDate = groceryList.CreatedDate,
            IsCompleted = groceryList.IsCompleted,
            CreatedAt = groceryList.CreatedAt
        };
    }
}
