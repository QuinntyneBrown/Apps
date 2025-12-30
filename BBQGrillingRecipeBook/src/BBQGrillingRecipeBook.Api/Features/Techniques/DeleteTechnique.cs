// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Techniques;

/// <summary>
/// Command to delete a technique.
/// </summary>
public class DeleteTechniqueCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the technique ID.
    /// </summary>
    public Guid TechniqueId { get; set; }
}

/// <summary>
/// Handler for DeleteTechniqueCommand.
/// </summary>
public class DeleteTechniqueCommandHandler : IRequestHandler<DeleteTechniqueCommand, bool>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTechniqueCommandHandler"/> class.
    /// </summary>
    public DeleteTechniqueCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteTechniqueCommand request, CancellationToken cancellationToken)
    {
        var technique = await _context.Techniques
            .FirstOrDefaultAsync(t => t.TechniqueId == request.TechniqueId, cancellationToken);

        if (technique == null)
        {
            return false;
        }

        _context.Techniques.Remove(technique);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
