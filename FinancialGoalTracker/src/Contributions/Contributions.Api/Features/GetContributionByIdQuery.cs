using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record GetContributionByIdQuery : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; init; }
}

public class GetContributionByIdQueryHandler : IRequestHandler<GetContributionByIdQuery, ContributionDto?>
{
    private readonly IContributionsDbContext _context;

    public GetContributionByIdQueryHandler(IContributionsDbContext context)
    {
        _context = context;
    }

    public async Task<ContributionDto?> Handle(GetContributionByIdQuery request, CancellationToken cancellationToken)
    {
        var contribution = await _context.Contributions
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null) return null;

        return new ContributionDto
        {
            ContributionId = contribution.ContributionId,
            GoalId = contribution.GoalId,
            Amount = contribution.Amount,
            ContributionDate = contribution.ContributionDate,
            Notes = contribution.Notes,
            CreatedAt = contribution.CreatedAt
        };
    }
}
