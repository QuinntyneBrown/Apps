using FinancialGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Api.Features.Contributions;

public record GetContributionByIdQuery : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; init; }
}

public class GetContributionByIdQueryHandler : IRequestHandler<GetContributionByIdQuery, ContributionDto?>
{
    private readonly IFinancialGoalTrackerContext _context;
    private readonly ILogger<GetContributionByIdQueryHandler> _logger;

    public GetContributionByIdQueryHandler(
        IFinancialGoalTrackerContext context,
        ILogger<GetContributionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto?> Handle(GetContributionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contribution {ContributionId}", request.ContributionId);

        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found", request.ContributionId);
            return null;
        }

        return contribution.ToDto();
    }
}
