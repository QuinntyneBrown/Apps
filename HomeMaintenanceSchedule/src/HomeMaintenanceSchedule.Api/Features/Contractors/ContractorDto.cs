using HomeMaintenanceSchedule.Core;

namespace HomeMaintenanceSchedule.Api.Features.Contractors;

public record ContractorDto
{
    public Guid ContractorId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Specialty { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public string? Address { get; init; }
    public string? LicenseNumber { get; init; }
    public bool IsInsured { get; init; }
    public int? Rating { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ContractorExtensions
{
    public static ContractorDto ToDto(this Contractor contractor)
    {
        return new ContractorDto
        {
            ContractorId = contractor.ContractorId,
            UserId = contractor.UserId,
            Name = contractor.Name,
            Specialty = contractor.Specialty,
            PhoneNumber = contractor.PhoneNumber,
            Email = contractor.Email,
            Website = contractor.Website,
            Address = contractor.Address,
            LicenseNumber = contractor.LicenseNumber,
            IsInsured = contractor.IsInsured,
            Rating = contractor.Rating,
            Notes = contractor.Notes,
            IsActive = contractor.IsActive,
            CreatedAt = contractor.CreatedAt,
        };
    }
}
