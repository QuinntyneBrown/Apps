// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryLists;

public class GetGroceryListQuery : IRequest<GroceryListDto?>
{
    public Guid GroceryListId { get; set; }
}

public class GetGroceryListQueryHandler : IRequestHandler<GetGroceryListQuery, GroceryListDto?>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetGroceryListQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<GroceryListDto?> Handle(GetGroceryListQuery request, CancellationToken cancellationToken)
    {
        var groceryList = await _context.GroceryLists
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.GroceryListId == request.GroceryListId, cancellationToken);

        if (groceryList == null)
            return null;

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
