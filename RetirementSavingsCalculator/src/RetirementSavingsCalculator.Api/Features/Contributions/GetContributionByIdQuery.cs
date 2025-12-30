using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.Contributions;

public record GetContributionByIdQuery : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; init; }
}

public class GetContributionByIdQueryHandler : IRequestHandler<GetContributionByIdQuery, ContributionDto?>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<GetContributionByIdQueryHandler> _logger;

    public GetContributionByIdQueryHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<GetContributionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto?> Handle(GetContributionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contribution {ContributionId}", request.ContributionId);

        var contribution = await _context.Contributions
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found", request.ContributionId);
            return null;
        }

        return contribution.ToDto();
    }
}
