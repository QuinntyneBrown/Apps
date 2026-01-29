using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record GetContributionsQuery : IRequest<IEnumerable<ContributionDto>>
{
    public Guid? GoalId { get; init; }
}

public class GetContributionsQueryHandler : IRequestHandler<GetContributionsQuery, IEnumerable<ContributionDto>>
{
    private readonly IContributionsDbContext _context;

    public GetContributionsQueryHandler(IContributionsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContributionDto>> Handle(GetContributionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Contributions.AsNoTracking();

        if (request.GoalId.HasValue)
        {
            query = query.Where(c => c.GoalId == request.GoalId.Value);
        }

        return await query
            .OrderByDescending(c => c.ContributionDate)
            .Select(c => new ContributionDto
            {
                ContributionId = c.ContributionId,
                GoalId = c.GoalId,
                Amount = c.Amount,
                ContributionDate = c.ContributionDate,
                Notes = c.Notes,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
