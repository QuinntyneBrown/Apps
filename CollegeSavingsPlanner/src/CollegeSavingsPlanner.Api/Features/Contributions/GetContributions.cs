// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Contributions;

/// <summary>
/// Query to get all contributions.
/// </summary>
public class GetContributionsQuery : IRequest<List<ContributionDto>>
{
    public Guid? PlanId { get; set; }
}

/// <summary>
/// Handler for GetContributionsQuery.
/// </summary>
public class GetContributionsQueryHandler : IRequestHandler<GetContributionsQuery, List<ContributionDto>>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetContributionsQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<ContributionDto>> Handle(GetContributionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Contributions.AsQueryable();

        if (request.PlanId.HasValue)
        {
            query = query.Where(c => c.PlanId == request.PlanId.Value);
        }

        var contributions = await query
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
            .OrderByDescending(c => c.ContributionDate)
            .ToListAsync(cancellationToken);

        return contributions;
    }
}
