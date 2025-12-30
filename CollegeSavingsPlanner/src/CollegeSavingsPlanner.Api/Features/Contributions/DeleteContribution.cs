// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Contributions;

/// <summary>
/// Command to delete a contribution.
/// </summary>
public class DeleteContributionCommand : IRequest<bool>
{
    public Guid ContributionId { get; set; }
}

/// <summary>
/// Handler for DeleteContributionCommand.
/// </summary>
public class DeleteContributionCommandHandler : IRequestHandler<DeleteContributionCommand, bool>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public DeleteContributionCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            return false;
        }

        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
