using GiftRegistryOrganizer.Core;

namespace GiftRegistryOrganizer.Api.Features.Contributions;

public record ContributionDto
{
    public Guid ContributionId { get; init; }
    public Guid RegistryItemId { get; init; }
    public string ContributorName { get; init; } = string.Empty;
    public string? ContributorEmail { get; init; }
    public int Quantity { get; init; }
    public DateTime ContributedAt { get; init; }
}

public static class ContributionExtensions
{
    public static ContributionDto ToDto(this Contribution contribution)
    {
        return new ContributionDto
        {
            ContributionId = contribution.ContributionId,
            RegistryItemId = contribution.RegistryItemId,
            ContributorName = contribution.ContributorName,
            ContributorEmail = contribution.ContributorEmail,
            Quantity = contribution.Quantity,
            ContributedAt = contribution.ContributedAt,
        };
    }
}
