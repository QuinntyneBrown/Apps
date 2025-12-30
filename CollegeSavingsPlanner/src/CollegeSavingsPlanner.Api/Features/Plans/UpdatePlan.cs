// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Plans;

/// <summary>
/// Command to update an existing plan.
/// </summary>
public class UpdatePlanCommand : IRequest<PlanDto?>
{
    public Guid PlanId { get; set; }
    public UpdatePlanDto Plan { get; set; } = new();
}

/// <summary>
/// Handler for UpdatePlanCommand.
/// </summary>
public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, PlanDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public UpdatePlanCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<PlanDto?> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.Plans
            .FirstOrDefaultAsync(p => p.PlanId == request.PlanId, cancellationToken);

        if (plan == null)
        {
            return null;
        }

        plan.Name = request.Plan.Name;
        plan.State = request.Plan.State;
        plan.AccountNumber = request.Plan.AccountNumber;
        plan.CurrentBalance = request.Plan.CurrentBalance;
        plan.Administrator = request.Plan.Administrator;
        plan.IsActive = request.Plan.IsActive;
        plan.Notes = request.Plan.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new PlanDto
        {
            PlanId = plan.PlanId,
            Name = plan.Name,
            State = plan.State,
            AccountNumber = plan.AccountNumber,
            CurrentBalance = plan.CurrentBalance,
            OpenedDate = plan.OpenedDate,
            Administrator = plan.Administrator,
            IsActive = plan.IsActive,
            Notes = plan.Notes
        };
    }
}
