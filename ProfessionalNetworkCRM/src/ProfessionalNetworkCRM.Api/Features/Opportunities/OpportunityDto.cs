using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate;
using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate.Enums;

namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public record OpportunityDto
{
    public Guid OpportunityId { get; init; }
    public Guid ContactId { get; init; }
    public OpportunityType Type { get; init; }
    public string Description { get; init; } = string.Empty;
    public OpportunityStatus Status { get; init; }
    public decimal? Value { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class OpportunityExtensions
{
    public static OpportunityDto ToDto(this Opportunity opportunity)
    {
        return new OpportunityDto
        {
            OpportunityId = opportunity.OpportunityId,
            ContactId = opportunity.ContactId,
            Type = opportunity.Type,
            Description = opportunity.Description,
            Status = opportunity.Status,
            Value = opportunity.Value,
            Notes = opportunity.Notes,
            CreatedAt = opportunity.CreatedAt,
            UpdatedAt = opportunity.UpdatedAt,
        };
    }
}
