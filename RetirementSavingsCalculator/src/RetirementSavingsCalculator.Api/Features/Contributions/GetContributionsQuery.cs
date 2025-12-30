using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.Contributions;

public record GetContributionsQuery : IRequest<IEnumerable<ContributionDto>>
{
    public Guid? RetirementScenarioId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? AccountName { get; init; }
    public bool? IsEmployerMatch { get; init; }
    public decimal? MinAmount { get; init; }
    public decimal? MaxAmount { get; init; }
}

public class GetContributionsQueryHandler : IRequestHandler<GetContributionsQuery, IEnumerable<ContributionDto>>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<GetContributionsQueryHandler> _logger;

    public GetContributionsQueryHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<GetContributionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ContributionDto>> Handle(GetContributionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contributions for scenario {RetirementScenarioId}", request.RetirementScenarioId);

        var query = _context.Contributions.AsNoTracking();

        if (request.RetirementScenarioId.HasValue)
        {
            query = query.Where(c => c.RetirementScenarioId == request.RetirementScenarioId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(c => c.ContributionDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(c => c.ContributionDate <= request.EndDate.Value);
        }

        if (!string.IsNullOrEmpty(request.AccountName))
        {
            query = query.Where(c => c.AccountName.Contains(request.AccountName));
        }

        if (request.IsEmployerMatch.HasValue)
        {
            query = query.Where(c => c.IsEmployerMatch == request.IsEmployerMatch.Value);
        }

        if (request.MinAmount.HasValue)
        {
            query = query.Where(c => c.Amount >= request.MinAmount.Value);
        }

        if (request.MaxAmount.HasValue)
        {
            query = query.Where(c => c.Amount <= request.MaxAmount.Value);
        }

        var contributions = await query
            .OrderByDescending(c => c.ContributionDate)
            .ToListAsync(cancellationToken);

        return contributions.Select(c => c.ToDto());
    }
}
