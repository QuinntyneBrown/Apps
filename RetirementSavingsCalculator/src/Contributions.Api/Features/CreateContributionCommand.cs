using Contributions.Core;
using Contributions.Core.Models;
using MediatR;

namespace Contributions.Api.Features;

public record CreateContributionCommand(Guid TenantId, Guid RetirementScenarioId, decimal Amount, DateTime ContributionDate, string AccountName, bool IsEmployerMatch) : IRequest<ContributionDto>;

public class CreateContributionCommandHandler : IRequestHandler<CreateContributionCommand, ContributionDto>
{
    private readonly IContributionsDbContext _context;
    public CreateContributionCommandHandler(IContributionsDbContext context) => _context = context;
    public async Task<ContributionDto> Handle(CreateContributionCommand request, CancellationToken ct)
    {
        var contribution = new Contribution { ContributionId = Guid.NewGuid(), TenantId = request.TenantId, RetirementScenarioId = request.RetirementScenarioId, Amount = request.Amount, ContributionDate = request.ContributionDate, AccountName = request.AccountName, IsEmployerMatch = request.IsEmployerMatch };
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync(ct);
        return contribution.ToDto();
    }
}
