// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;

namespace MedicationReminderSystem.Api.Features.Medication;

/// <summary>
/// Data transfer object for Medication.
/// </summary>
public record MedicationDto
{
    public Guid MedicationId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public MedicationType MedicationType { get; init; }
    public string Dosage { get; init; } = string.Empty;
    public string? PrescribingDoctor { get; init; }
    public DateTime? PrescriptionDate { get; init; }
    public string? Purpose { get; init; }
    public string? Instructions { get; init; }
    public string? SideEffects { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Medication entity.
/// </summary>
public static class MedicationExtensions
{
    /// <summary>
    /// Converts a Medication entity to a MedicationDto.
    /// </summary>
    public static MedicationDto ToDto(this Core.Medication medication)
    {
        return new MedicationDto
        {
            MedicationId = medication.MedicationId,
            UserId = medication.UserId,
            Name = medication.Name,
            MedicationType = medication.MedicationType,
            Dosage = medication.Dosage,
            PrescribingDoctor = medication.PrescribingDoctor,
            PrescriptionDate = medication.PrescriptionDate,
            Purpose = medication.Purpose,
            Instructions = medication.Instructions,
            SideEffects = medication.SideEffects,
            IsActive = medication.IsActive,
            CreatedAt = medication.CreatedAt
        };
    }
}
