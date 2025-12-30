// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Contributions;

/// <summary>
/// Command to update an existing contribution.
/// </summary>
public class UpdateContributionCommand : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; set; }
    public UpdateContributionDto Contribution { get; set; } = new();
}

/// <summary>
/// Handler for UpdateContributionCommand.
/// </summary>
public class UpdateContributionCommandHandler : IRequestHandler<UpdateContributionCommand, ContributionDto?>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public UpdateContributionCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<ContributionDto?> Handle(UpdateContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            return null;
        }

        contribution.Amount = request.Contribution.Amount;
        contribution.ContributionDate = request.Contribution.ContributionDate;
        contribution.Contributor = request.Contribution.Contributor;
        contribution.Notes = request.Contribution.Notes;
        contribution.IsRecurring = request.Contribution.IsRecurring;

        contribution.ValidateAmount();

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
