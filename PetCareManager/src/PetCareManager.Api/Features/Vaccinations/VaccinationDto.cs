using PetCareManager.Core;

namespace PetCareManager.Api.Features.Vaccinations;

public record VaccinationDto
{
    public Guid VaccinationId { get; init; }
    public Guid PetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime DateAdministered { get; init; }
    public DateTime? NextDueDate { get; init; }
    public string? VetName { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class VaccinationExtensions
{
    public static VaccinationDto ToDto(this Vaccination vaccination)
    {
        return new VaccinationDto
        {
            VaccinationId = vaccination.VaccinationId,
            PetId = vaccination.PetId,
            Name = vaccination.Name,
            DateAdministered = vaccination.DateAdministered,
            NextDueDate = vaccination.NextDueDate,
            VetName = vaccination.VetName,
            CreatedAt = vaccination.CreatedAt,
        };
    }
}
