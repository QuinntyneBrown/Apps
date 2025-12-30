using PetCareManager.Core;

namespace PetCareManager.Api.Features.Medications;

public record MedicationDto
{
    public Guid MedicationId { get; init; }
    public Guid PetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Dosage { get; init; }
    public string? Frequency { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class MedicationExtensions
{
    public static MedicationDto ToDto(this Medication medication)
    {
        return new MedicationDto
        {
            MedicationId = medication.MedicationId,
            PetId = medication.PetId,
            Name = medication.Name,
            Dosage = medication.Dosage,
            Frequency = medication.Frequency,
            StartDate = medication.StartDate,
            EndDate = medication.EndDate,
            CreatedAt = medication.CreatedAt,
        };
    }
}
