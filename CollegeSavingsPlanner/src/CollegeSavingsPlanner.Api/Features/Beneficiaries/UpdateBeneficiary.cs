// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Beneficiaries;

/// <summary>
/// Command to update an existing beneficiary.
/// </summary>
public class UpdateBeneficiaryCommand : IRequest<BeneficiaryDto?>
{
    public Guid BeneficiaryId { get; set; }
    public UpdateBeneficiaryDto Beneficiary { get; set; } = new();
}

/// <summary>
/// Handler for UpdateBeneficiaryCommand.
/// </summary>
public class UpdateBeneficiaryCommandHandler : IRequestHandler<UpdateBeneficiaryCommand, BeneficiaryDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public UpdateBeneficiaryCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<BeneficiaryDto?> Handle(UpdateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var beneficiary = await _context.Beneficiaries
            .FirstOrDefaultAsync(b => b.BeneficiaryId == request.BeneficiaryId, cancellationToken);

        if (beneficiary == null)
        {
            return null;
        }

        beneficiary.FirstName = request.Beneficiary.FirstName;
        beneficiary.LastName = request.Beneficiary.LastName;
        beneficiary.DateOfBirth = request.Beneficiary.DateOfBirth;
        beneficiary.Relationship = request.Beneficiary.Relationship;
        beneficiary.ExpectedCollegeStartYear = request.Beneficiary.ExpectedCollegeStartYear;
        beneficiary.IsPrimary = request.Beneficiary.IsPrimary;

        await _context.SaveChangesAsync(cancellationToken);

        return new BeneficiaryDto
        {
            BeneficiaryId = beneficiary.BeneficiaryId,
            PlanId = beneficiary.PlanId,
            FirstName = beneficiary.FirstName,
            LastName = beneficiary.LastName,
            DateOfBirth = beneficiary.DateOfBirth,
            Relationship = beneficiary.Relationship,
            ExpectedCollegeStartYear = beneficiary.ExpectedCollegeStartYear,
            IsPrimary = beneficiary.IsPrimary,
            Age = beneficiary.CalculateAge(),
            YearsUntilCollege = beneficiary.CalculateYearsUntilCollege()
        };
    }
}
