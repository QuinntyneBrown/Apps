using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Contributions;

public record GetContributionsQuery : IRequest<IEnumerable<ContributionDto>>
{
    public Guid? GoalId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
}

public class GetContributionsQueryHandler : IRequestHandler<GetContributionsQuery, IEnumerable<ContributionDto>>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<GetContributionsQueryHandler> _logger;

    public GetContributionsQueryHandler(
        IFinancialGoalTrackerContext context,
        ILogger<GetContributionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ContributionDto>> Handle(GetContributionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contributions with filters - GoalId: {GoalId}, FromDate: {FromDate}, ToDate: {ToDate}",
            request.GoalId,
            request.FromDate,
            request.ToDate);

        var query = _context.Contributions.AsQueryable();

        if (request.GoalId.HasValue)
        {
            query = query.Where(c => c.GoalId == request.GoalId.Value);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(c => c.ContributionDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(c => c.ContributionDate <= request.ToDate.Value);
        }

        var contributions = await query
            .OrderByDescending(c => c.ContributionDate)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} contributions", contributions.Count);

        return contributions.Select(c => c.ToDto());
    }
}
