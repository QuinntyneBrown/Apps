// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Plans;

/// <summary>
/// Query to get all plans.
/// </summary>
public class GetPlansQuery : IRequest<List<PlanDto>>
{
}

/// <summary>
/// Handler for GetPlansQuery.
/// </summary>
public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, List<PlanDto>>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetPlansQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<PlanDto>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await _context.Plans
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
            .ToListAsync(cancellationToken);

        return plans;
    }
}
