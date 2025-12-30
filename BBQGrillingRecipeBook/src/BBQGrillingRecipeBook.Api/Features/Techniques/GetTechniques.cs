// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Techniques;

/// <summary>
/// Query to get all techniques.
/// </summary>
public class GetTechniquesQuery : IRequest<List<TechniqueDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetTechniquesQuery.
/// </summary>
public class GetTechniquesQueryHandler : IRequestHandler<GetTechniquesQuery, List<TechniqueDto>>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTechniquesQueryHandler"/> class.
    /// </summary>
    public GetTechniquesQueryHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<TechniqueDto>> Handle(GetTechniquesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Techniques.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        var techniques = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return techniques.Select(TechniqueDto.FromEntity).ToList();
    }
}
