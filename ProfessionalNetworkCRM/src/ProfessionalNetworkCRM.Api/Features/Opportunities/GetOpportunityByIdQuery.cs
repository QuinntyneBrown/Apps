using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public record GetOpportunityByIdQuery : IRequest<OpportunityDto?>
{
    public Guid OpportunityId { get; init; }
}

public class GetOpportunityByIdQueryHandler : IRequestHandler<GetOpportunityByIdQuery, OpportunityDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetOpportunityByIdQueryHandler> _logger;

    public GetOpportunityByIdQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetOpportunityByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<OpportunityDto?> Handle(GetOpportunityByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting opportunity {OpportunityId}", request.OpportunityId);

        var opportunity = await _context.Opportunities
            .FirstOrDefaultAsync(o => o.OpportunityId == request.OpportunityId, cancellationToken);

        if (opportunity == null)
        {
            _logger.LogWarning("Opportunity {OpportunityId} not found", request.OpportunityId);
            return null;
        }

        return opportunity.ToDto();
    }
}
