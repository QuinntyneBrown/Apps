using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Model.OpportunityAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public record GetOpportunitiesQuery : IRequest<IEnumerable<OpportunityDto>>
{
    public Guid? ContactId { get; init; }
    public OpportunityType? OpportunityType { get; init; }
    public OpportunityStatus? Status { get; init; }
}

public class GetOpportunitiesQueryHandler : IRequestHandler<GetOpportunitiesQuery, IEnumerable<OpportunityDto>>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetOpportunitiesQueryHandler> _logger;

    public GetOpportunitiesQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetOpportunitiesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<OpportunityDto>> Handle(GetOpportunitiesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting opportunities with filters: ContactId={ContactId}, Type={Type}, Status={Status}",
            request.ContactId, request.OpportunityType, request.Status);

        var query = _context.Opportunities.AsQueryable();

        if (request.ContactId.HasValue)
        {
            query = query.Where(o => o.ContactId == request.ContactId.Value);
        }

        if (request.OpportunityType.HasValue)
        {
            query = query.Where(o => o.Type == request.OpportunityType.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(o => o.Status == request.Status.Value);
        }

        var opportunities = await query
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} opportunities", opportunities.Count);

        return opportunities.Select(o => o.ToDto());
    }
}
