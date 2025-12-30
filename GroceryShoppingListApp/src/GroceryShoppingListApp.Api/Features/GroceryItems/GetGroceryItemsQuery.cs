// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryItems;

public class GetGroceryItemsQuery : IRequest<List<GroceryItemDto>>
{
    public Guid? GroceryListId { get; set; }
    public Category? Category { get; set; }
}

public class GetGroceryItemsQueryHandler : IRequestHandler<GetGroceryItemsQuery, List<GroceryItemDto>>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetGroceryItemsQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<List<GroceryItemDto>> Handle(GetGroceryItemsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.GroceryItems.AsNoTracking();

        if (request.GroceryListId.HasValue)
        {
            query = query.Where(x => x.GroceryListId == request.GroceryListId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(x => x.Category == request.Category.Value);
        }

        var groceryItems = await query.ToListAsync(cancellationToken);

        return groceryItems.Select(gi => new GroceryItemDto
        {
            GroceryItemId = gi.GroceryItemId,
            GroceryListId = gi.GroceryListId,
            Name = gi.Name,
            Category = gi.Category,
            Quantity = gi.Quantity,
            IsChecked = gi.IsChecked,
            CreatedAt = gi.CreatedAt
        }).ToList();
    }
}
