using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.Contributions;

public record UpdateContributionCommand : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public bool IsEmployerMatch { get; init; }
    public string? Notes { get; init; }
}

public class UpdateContributionCommandHandler : IRequestHandler<UpdateContributionCommand, ContributionDto?>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<UpdateContributionCommandHandler> _logger;

    public UpdateContributionCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<UpdateContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto?> Handle(UpdateContributionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating contribution {ContributionId}", request.ContributionId);

        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found", request.ContributionId);
            return null;
        }

        contribution.Amount = request.Amount;
        contribution.ContributionDate = request.ContributionDate;
        contribution.AccountName = request.AccountName;
        contribution.IsEmployerMatch = request.IsEmployerMatch;
        contribution.Notes = request.Notes;

        contribution.ValidateAmount();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated contribution {ContributionId}", request.ContributionId);

        return contribution.ToDto();
    }
}
