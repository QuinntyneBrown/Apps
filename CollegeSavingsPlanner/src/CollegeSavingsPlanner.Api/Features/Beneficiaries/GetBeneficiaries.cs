// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Beneficiaries;

/// <summary>
/// Query to get all beneficiaries.
/// </summary>
public class GetBeneficiariesQuery : IRequest<List<BeneficiaryDto>>
{
    public Guid? PlanId { get; set; }
}

/// <summary>
/// Handler for GetBeneficiariesQuery.
/// </summary>
public class GetBeneficiariesQueryHandler : IRequestHandler<GetBeneficiariesQuery, List<BeneficiaryDto>>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetBeneficiariesQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<BeneficiaryDto>> Handle(GetBeneficiariesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Beneficiaries.AsQueryable();

        if (request.PlanId.HasValue)
        {
            query = query.Where(b => b.PlanId == request.PlanId.Value);
        }

        var beneficiaries = await query
            .Select(b => new BeneficiaryDto
            {
                BeneficiaryId = b.BeneficiaryId,
                PlanId = b.PlanId,
                FirstName = b.FirstName,
                LastName = b.LastName,
                DateOfBirth = b.DateOfBirth,
                Relationship = b.Relationship,
                ExpectedCollegeStartYear = b.ExpectedCollegeStartYear,
                IsPrimary = b.IsPrimary,
                Age = b.CalculateAge(),
                YearsUntilCollege = b.CalculateYearsUntilCollege()
            })
            .ToListAsync(cancellationToken);

        return beneficiaries;
    }
}
