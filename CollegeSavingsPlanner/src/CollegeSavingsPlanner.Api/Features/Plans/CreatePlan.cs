// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;

namespace CollegeSavingsPlanner.Api.Features.Plans;

/// <summary>
/// Command to create a new plan.
/// </summary>
public class CreatePlanCommand : IRequest<PlanDto>
{
    public CreatePlanDto Plan { get; set; } = new();
}

/// <summary>
/// Handler for CreatePlanCommand.
/// </summary>
public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, PlanDto>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public CreatePlanCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<PlanDto> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = new Plan
        {
            PlanId = Guid.NewGuid(),
            Name = request.Plan.Name,
            State = request.Plan.State,
            AccountNumber = request.Plan.AccountNumber,
            CurrentBalance = request.Plan.CurrentBalance,
            OpenedDate = request.Plan.OpenedDate,
            Administrator = request.Plan.Administrator,
            IsActive = true,
            Notes = request.Plan.Notes
        };

        _context.Plans.Add(plan);
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
