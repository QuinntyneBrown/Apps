using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.Contributions;

public record CreateContributionCommand : IRequest<ContributionDto>
{
    public Guid RetirementScenarioId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public bool IsEmployerMatch { get; init; }
    public string? Notes { get; init; }
}

public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, ContributionDto>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<CreateContributionCommandHandler> _logger;

    public CreateContributionCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<CreateContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto> Handle(CreateContributionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating contribution of {Amount} for scenario {RetirementScenarioId}",
            request.Amount,
            request.RetirementScenarioId);

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            RetirementScenarioId = request.RetirementScenarioId,
            Amount = request.Amount,
            ContributionDate = request.ContributionDate,
            AccountName = request.AccountName,
            IsEmployerMatch = request.IsEmployerMatch,
            Notes = request.Notes,
        };

        contribution.ValidateAmount();

        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created contribution {ContributionId}",
            contribution.ContributionId);

        return contribution.ToDto();
    }
}
