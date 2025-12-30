// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryLists;

public class UpdateGroceryListCommand : IRequest<GroceryListDto>
{
    public Guid GroceryListId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

public class UpdateGroceryListCommandHandler : IRequestHandler<UpdateGroceryListCommand, GroceryListDto>
{
    private readonly IGroceryShoppingListAppContext _context;

    public UpdateGroceryListCommandHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<GroceryListDto> Handle(UpdateGroceryListCommand request, CancellationToken cancellationToken)
    {
        var groceryList = await _context.GroceryLists
            .FirstOrDefaultAsync(x => x.GroceryListId == request.GroceryListId, cancellationToken)
            ?? throw new InvalidOperationException($"GroceryList with ID {request.GroceryListId} not found");

        groceryList.Name = request.Name;
        groceryList.IsCompleted = request.IsCompleted;

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
