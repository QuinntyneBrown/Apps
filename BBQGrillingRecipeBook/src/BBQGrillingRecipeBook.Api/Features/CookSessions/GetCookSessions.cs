// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.CookSessions;

/// <summary>
/// Query to get all cook sessions.
/// </summary>
public class GetCookSessionsQuery : IRequest<List<CookSessionDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional recipe ID filter.
    /// </summary>
    public Guid? RecipeId { get; set; }
}

/// <summary>
/// Handler for GetCookSessionsQuery.
/// </summary>
public class GetCookSessionsQueryHandler : IRequestHandler<GetCookSessionsQuery, List<CookSessionDto>>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCookSessionsQueryHandler"/> class.
    /// </summary>
    public GetCookSessionsQueryHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<CookSessionDto>> Handle(GetCookSessionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.CookSessions.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.RecipeId.HasValue)
        {
            query = query.Where(s => s.RecipeId == request.RecipeId.Value);
        }

        var sessions = await query
            .OrderByDescending(s => s.CookDate)
            .ToListAsync(cancellationToken);

        return sessions.Select(CookSessionDto.FromEntity).ToList();
    }
}
