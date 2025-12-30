// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Beneficiaries;

/// <summary>
/// Query to get a beneficiary by ID.
/// </summary>
public class GetBeneficiaryByIdQuery : IRequest<BeneficiaryDto?>
{
    public Guid BeneficiaryId { get; set; }
}

/// <summary>
/// Handler for GetBeneficiaryByIdQuery.
/// </summary>
public class GetBeneficiaryByIdQueryHandler : IRequestHandler<GetBeneficiaryByIdQuery, BeneficiaryDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public GetBeneficiaryByIdQueryHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<BeneficiaryDto?> Handle(GetBeneficiaryByIdQuery request, CancellationToken cancellationToken)
    {
        var beneficiary = await _context.Beneficiaries
            .Where(b => b.BeneficiaryId == request.BeneficiaryId)
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
            .FirstOrDefaultAsync(cancellationToken);

        return beneficiary;
    }
}
