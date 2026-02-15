using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record UpdateContributionCommand : IRequest<ContributionDto?>
{
    public Guid ContributionId { get; init; }
    public decimal? Amount { get; init; }
    public DateTime? ContributionDate { get; init; }
    public string? Notes { get; init; }
}

public class UpdateContributionCommandHandler : IRequestHandler<UpdateContributionCommand, ContributionDto?>
{
    private readonly IContributionsDbContext _context;
    private readonly ILogger<UpdateContributionCommandHandler> _logger;

    public UpdateContributionCommandHandler(IContributionsDbContext context, ILogger<UpdateContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContributionDto?> Handle(UpdateContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null) return null;

        contribution.Update(request.Amount, request.ContributionDate, request.Notes);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Contribution updated: {ContributionId}", contribution.ContributionId);

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
