// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingListApp.Api.Features.GroceryLists;

public class GetGroceryListsQuery : IRequest<List<GroceryListDto>>
{
    public Guid? UserId { get; set; }
}

public class GetGroceryListsQueryHandler : IRequestHandler<GetGroceryListsQuery, List<GroceryListDto>>
{
    private readonly IGroceryShoppingListAppContext _context;

    public GetGroceryListsQueryHandler(IGroceryShoppingListAppContext context)
    {
        _context = context;
    }

    public async Task<List<GroceryListDto>> Handle(GetGroceryListsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.GroceryLists.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        var groceryLists = await query.ToListAsync(cancellationToken);

        return groceryLists.Select(gl => new GroceryListDto
        {
            GroceryListId = gl.GroceryListId,
            UserId = gl.UserId,
            Name = gl.Name,
            CreatedDate = gl.CreatedDate,
            IsCompleted = gl.IsCompleted,
            CreatedAt = gl.CreatedAt
        }).ToList();
    }
}
