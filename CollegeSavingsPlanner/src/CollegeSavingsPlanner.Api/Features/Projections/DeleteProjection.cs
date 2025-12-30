// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Projections;

/// <summary>
/// Command to delete a projection.
/// </summary>
public class DeleteProjectionCommand : IRequest<bool>
{
    public Guid ProjectionId { get; set; }
}

/// <summary>
/// Handler for DeleteProjectionCommand.
/// </summary>
public class DeleteProjectionCommandHandler : IRequestHandler<DeleteProjectionCommand, bool>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public DeleteProjectionCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProjectionCommand request, CancellationToken cancellationToken)
    {
        var projection = await _context.Projections
            .FirstOrDefaultAsync(p => p.ProjectionId == request.ProjectionId, cancellationToken);

        if (projection == null)
        {
            return false;
        }

        _context.Projections.Remove(projection);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
