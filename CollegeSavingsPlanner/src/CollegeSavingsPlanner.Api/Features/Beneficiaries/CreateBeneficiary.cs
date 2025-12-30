// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;

namespace CollegeSavingsPlanner.Api.Features.Beneficiaries;

/// <summary>
/// Command to create a new beneficiary.
/// </summary>
public class CreateBeneficiaryCommand : IRequest<BeneficiaryDto>
{
    public CreateBeneficiaryDto Beneficiary { get; set; } = new();
}

/// <summary>
/// Handler for CreateBeneficiaryCommand.
/// </summary>
public class CreateBeneficiaryCommandHandler : IRequestHandler<CreateBeneficiaryCommand, BeneficiaryDto>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public CreateBeneficiaryCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<BeneficiaryDto> Handle(CreateBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var beneficiary = new Beneficiary
        {
            BeneficiaryId = Guid.NewGuid(),
            PlanId = request.Beneficiary.PlanId,
            FirstName = request.Beneficiary.FirstName,
            LastName = request.Beneficiary.LastName,
            DateOfBirth = request.Beneficiary.DateOfBirth,
            Relationship = request.Beneficiary.Relationship,
            ExpectedCollegeStartYear = request.Beneficiary.ExpectedCollegeStartYear,
            IsPrimary = request.Beneficiary.IsPrimary
        };

        _context.Beneficiaries.Add(beneficiary);
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
