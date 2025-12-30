// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Plans;

/// <summary>
/// Query to get a plan by ID.
/// </summary>
public class GetPlanByIdQuery : IRequest<PlanDto?>
{
    public Guid PlanId { get; set; }
}

/// <summary>
/// Handler for GetPlanByIdQuery.
/// </summary>
public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, PlanDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetPlanByIdQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<PlanDto?> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var plan = await _context.Plans
            .Where(p => p.PlanId == request.PlanId)
            .Select(p => new PlanDto
            {
                PlanId = p.PlanId,
                Name = p.Name,
                State = p.State,
                AccountNumber = p.AccountNumber,
                CurrentBalance = p.CurrentBalance,
                OpenedDate = p.OpenedDate,
                Administrator = p.Administrator,
                IsActive = p.IsActive,
                Notes = p.Notes
            })
            .FirstOrDefaultAsync(cancellationToken);

        return plan;
    }
}
