// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Medication;

/// <summary>
/// Command to update an existing medication.
/// </summary>
public record UpdateMedicationCommand : IRequest<MedicationDto>
{
    public Guid MedicationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public MedicationType MedicationType { get; init; }
    public string Dosage { get; init; } = string.Empty;
    public string? PrescribingDoctor { get; init; }
    public DateTime? PrescriptionDate { get; init; }
    public string? Purpose { get; init; }
    public string? Instructions { get; init; }
    public string? SideEffects { get; init; }
    public bool IsActive { get; init; }
}

/// <summary>
/// Handler for UpdateMedicationCommand.
/// </summary>
public class UpdateMedicationCommandHandler : IRequestHandler<UpdateMedicationCommand, MedicationDto>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<UpdateMedicationCommandHandler> _logger;

    public UpdateMedicationCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<UpdateMedicationCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<MedicationDto> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating medication: {MedicationId}", request.MedicationId);

        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == request.MedicationId, cancellationToken);

        if (medication == null)
        {
            throw new InvalidOperationException($"Medication with ID {request.MedicationId} not found.");
        }

        medication.Name = request.Name;
        medication.MedicationType = request.MedicationType;
        medication.Dosage = request.Dosage;
        medication.PrescribingDoctor = request.PrescribingDoctor;
        medication.PrescriptionDate = request.PrescriptionDate;
        medication.Purpose = request.Purpose;
        medication.Instructions = request.Instructions;
        medication.SideEffects = request.SideEffects;
        medication.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Medication updated: {MedicationId}", medication.MedicationId);

        return medication.ToDto();
    }
}
