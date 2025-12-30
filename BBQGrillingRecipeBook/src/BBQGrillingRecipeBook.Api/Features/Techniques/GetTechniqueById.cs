// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Techniques;

/// <summary>
/// Query to get a technique by ID.
/// </summary>
public class GetTechniqueByIdQuery : IRequest<TechniqueDto?>
{
    /// <summary>
    /// Gets or sets the technique ID.
    /// </summary>
    public Guid TechniqueId { get; set; }
}

/// <summary>
/// Handler for GetTechniqueByIdQuery.
/// </summary>
public class GetTechniqueByIdQueryHandler : IRequestHandler<GetTechniqueByIdQuery, TechniqueDto?>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTechniqueByIdQueryHandler"/> class.
    /// </summary>
    public GetTechniqueByIdQueryHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TechniqueDto?> Handle(GetTechniqueByIdQuery request, CancellationToken cancellationToken)
    {
        var technique = await _context.Techniques
            .FirstOrDefaultAsync(t => t.TechniqueId == request.TechniqueId, cancellationToken);

        return technique == null ? null : TechniqueDto.FromEntity(technique);
    }
}
