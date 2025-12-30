// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;

namespace CollegeSavingsPlanner.Api.Features.Contributions;

/// <summary>
/// Command to create a new contribution.
/// </summary>
public class CreateContributionCommand : IRequest<ContributionDto>
{
    public CreateContributionDto Contribution { get; set; } = new();
}

/// <summary>
/// Handler for CreateContributionCommand.
/// </summary>
public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, ContributionDto>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public CreateContributionCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<ContributionDto> Handle(CreateContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            PlanId = request.Contribution.PlanId,
            Amount = request.Contribution.Amount,
            ContributionDate = request.Contribution.ContributionDate,
            Contributor = request.Contribution.Contributor,
            Notes = request.Contribution.Notes,
            IsRecurring = request.Contribution.IsRecurring
        };

        contribution.ValidateAmount();

        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync(cancellationToken);

        return new ContributionDto
        {
            ContributionId = contribution.ContributionId,
            PlanId = contribution.PlanId,
            Amount = contribution.Amount,
            ContributionDate = contribution.ContributionDate,
            Contributor = contribution.Contributor,
            Notes = contribution.Notes,
            IsRecurring = contribution.IsRecurring
        };
    }
}
