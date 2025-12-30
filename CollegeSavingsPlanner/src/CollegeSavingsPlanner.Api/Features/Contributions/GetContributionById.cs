// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Contributions;

/// <summary>
/// Query to get a contribution by ID.
/// </summary>
public class GetContributionByIdQuery : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; set; }
}

/// <summary>
/// Handler for GetContributionByIdQuery.
/// </summary>
public class GetContributionByIdQueryHandler : IRequestHandler<GetContributionByIdQuery, ContributionDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetContributionByIdQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<ContributionDto?> Handle(GetContributionByIdQuery request, CancellationToken cancellationToken)
    {
        var contribution = await _context.Contributions
            .Where(c => c.ContributionId == request.ContributionId)
            .Select(c => new ContributionDto
            {
                ContributionId = c.ContributionId,
                PlanId = c.PlanId,
                Amount = c.Amount,
                ContributionDate = c.ContributionDate,
                Contributor = c.Contributor,
                Notes = c.Notes,
                IsRecurring = c.IsRecurring
            })
            .FirstOrDefaultAsync(cancellationToken);

        return contribution;
    }
}
