using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Model.OpportunityAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public record UpdateOpportunityCommand : IRequest<OpportunityDto?>
{
    public Guid OpportunityId { get; init; }
    public string Description { get; init; } = string.Empty;
    public OpportunityStatus Status { get; init; }
    public string? PotentialValue { get; init; }
    public string? Notes { get; init; }
}

public class UpdateOpportunityCommandHandler : IRequestHandler<UpdateOpportunityCommand, OpportunityDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<UpdateOpportunityCommandHandler> _logger;

    public UpdateOpportunityCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<UpdateOpportunityCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<OpportunityDto?> Handle(UpdateOpportunityCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating opportunity {OpportunityId}", request.OpportunityId);

        var opportunity = await _context.Opportunities
            .FirstOrDefaultAsync(o => o.OpportunityId == request.OpportunityId, cancellationToken);

        if (opportunity == null)
        {
            _logger.LogWarning("Opportunity {OpportunityId} not found", request.OpportunityId);
            return null;
        }

        opportunity.Description = request.Description;
        opportunity.UpdateStatus(request.Status);
        opportunity.Notes = request.Notes;

        decimal? value = null;
        if (!string.IsNullOrEmpty(request.PotentialValue) && decimal.TryParse(request.PotentialValue, out var parsedValue))
        {
            value = parsedValue;
        }
        opportunity.UpdateValue(value);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated opportunity {OpportunityId}", request.OpportunityId);

        return opportunity.ToDto();
    }
}
