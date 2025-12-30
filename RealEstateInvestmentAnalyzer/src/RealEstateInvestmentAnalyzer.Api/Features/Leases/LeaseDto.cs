using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record LeaseDto
{
    public Guid LeaseId { get; init; }
    public Guid PropertyId { get; init; }
    public string TenantName { get; init; } = string.Empty;
    public decimal MonthlyRent { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal SecurityDeposit { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public static class LeaseExtensions
{
    public static LeaseDto ToDto(this Lease lease)
    {
        return new LeaseDto
        {
            LeaseId = lease.LeaseId,
            PropertyId = lease.PropertyId,
            TenantName = lease.TenantName,
            MonthlyRent = lease.MonthlyRent,
            StartDate = lease.StartDate,
            EndDate = lease.EndDate,
            SecurityDeposit = lease.SecurityDeposit,
            IsActive = lease.IsActive,
            Notes = lease.Notes,
        };
    }
}
