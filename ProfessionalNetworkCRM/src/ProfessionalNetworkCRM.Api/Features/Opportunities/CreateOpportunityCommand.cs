using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate;
using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public record CreateOpportunityCommand : IRequest<OpportunityDto>
{
    public Guid ContactId { get; init; }
    public OpportunityType OpportunityType { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? PotentialValue { get; init; }
    public OpportunityStatus Status { get; init; }
    public string? Notes { get; init; }
}

public class CreateOpportunityCommandHandler : IRequestHandler<CreateOpportunityCommand, OpportunityDto>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<CreateOpportunityCommandHandler> _logger;
    private readonly ITenantContext _tenantContext;

    public CreateOpportunityCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<CreateOpportunityCommandHandler> logger,
        ITenantContext tenantContext)
    {
        _context = context;
        _logger = logger;
        _tenantContext = tenantContext;
    }

    public async Task<OpportunityDto> Handle(CreateOpportunityCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating opportunity for contact {ContactId}, type: {OpportunityType}",
            request.ContactId,
            request.OpportunityType);

        var opportunity = new Opportunity
        {
            OpportunityId = Guid.NewGuid(),
            ContactId = request.ContactId,
            TenantId = _tenantContext.TenantId,
            Type = request.OpportunityType,
            Description = request.Description,
            Status = request.Status,
            Value = OpportunityHelpers.ParseValue(request.PotentialValue),
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Opportunities.Add(opportunity);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created opportunity {OpportunityId} for contact {ContactId}",
            opportunity.OpportunityId,
            request.ContactId);

        return opportunity.ToDto();
    }
}
