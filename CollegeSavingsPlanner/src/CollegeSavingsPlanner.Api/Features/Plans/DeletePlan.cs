// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Plans;

/// <summary>
/// Command to delete a plan.
/// </summary>
public class DeletePlanCommand : IRequest<bool>
{
    public Guid PlanId { get; set; }
}

/// <summary>
/// Handler for DeletePlanCommand.
/// </summary>
public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, bool>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public DeletePlanCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.Plans
            .FirstOrDefaultAsync(p => p.PlanId == request.PlanId, cancellationToken);

        if (plan == null)
        {
            return false;
        }

        _context.Plans.Remove(plan);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
