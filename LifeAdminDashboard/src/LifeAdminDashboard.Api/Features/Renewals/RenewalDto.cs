using LifeAdminDashboard.Core;

namespace LifeAdminDashboard.Api.Features.Renewals;

public record RenewalDto
{
    public Guid RenewalId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string RenewalType { get; init; } = string.Empty;
    public string? Provider { get; init; }
    public DateTime RenewalDate { get; init; }
    public decimal? Cost { get; init; }
    public string Frequency { get; init; } = string.Empty;
    public bool IsAutoRenewal { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class RenewalExtensions
{
    public static RenewalDto ToDto(this Renewal renewal)
    {
        return new RenewalDto
        {
            RenewalId = renewal.RenewalId,
            UserId = renewal.UserId,
            Name = renewal.Name,
            RenewalType = renewal.RenewalType,
            Provider = renewal.Provider,
            RenewalDate = renewal.RenewalDate,
            Cost = renewal.Cost,
            Frequency = renewal.Frequency,
            IsAutoRenewal = renewal.IsAutoRenewal,
            IsActive = renewal.IsActive,
            Notes = renewal.Notes,
            CreatedAt = renewal.CreatedAt,
        };
    }
}
