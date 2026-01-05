using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public record DeleteOpportunityCommand : IRequest<bool>
{
    public Guid OpportunityId { get; init; }
}

public class DeleteOpportunityCommandHandler : IRequestHandler<DeleteOpportunityCommand, bool>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<DeleteOpportunityCommandHandler> _logger;

    public DeleteOpportunityCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<DeleteOpportunityCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOpportunityCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting opportunity {OpportunityId}", request.OpportunityId);

        var opportunity = await _context.Opportunities
            .FirstOrDefaultAsync(o => o.OpportunityId == request.OpportunityId, cancellationToken);

        if (opportunity == null)
        {
            _logger.LogWarning("Opportunity {OpportunityId} not found", request.OpportunityId);
            return false;
        }

        _context.Opportunities.Remove(opportunity);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted opportunity {OpportunityId}", request.OpportunityId);

        return true;
    }
}
