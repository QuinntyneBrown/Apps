using ProfessionalNetworkCRM.Core.Models.ReferralAggregate;

namespace ProfessionalNetworkCRM.Api.Features.Referrals;

public record ReferralDto
{
    public Guid ReferralId { get; init; }
    public Guid SourceContactId { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? Outcome { get; init; }
    public string? Notes { get; init; }
    public bool ThankYouSent { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ReferralExtensions
{
    public static ReferralDto ToDto(this Referral referral)
    {
        return new ReferralDto
        {
            ReferralId = referral.ReferralId,
            SourceContactId = referral.SourceContactId,
            Description = referral.Description,
            Outcome = referral.Outcome,
            Notes = referral.Notes,
            ThankYouSent = referral.ThankYouSent,
            CreatedAt = referral.CreatedAt,
            UpdatedAt = referral.UpdatedAt,
        };
    }
}
