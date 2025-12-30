// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Medication;

/// <summary>
/// Command to create a new medication.
/// </summary>
public record CreateMedicationCommand : IRequest<MedicationDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public MedicationType MedicationType { get; init; }
    public string Dosage { get; init; } = string.Empty;
    public string? PrescribingDoctor { get; init; }
    public DateTime? PrescriptionDate { get; init; }
    public string? Purpose { get; init; }
    public string? Instructions { get; init; }
    public string? SideEffects { get; init; }
}

/// <summary>
/// Handler for CreateMedicationCommand.
/// </summary>
public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, MedicationDto>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<CreateMedicationCommandHandler> _logger;

    public CreateMedicationCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<CreateMedicationCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<MedicationDto> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating medication: {Name}", request.Name);

        var medication = new Core.Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            MedicationType = request.MedicationType,
            Dosage = request.Dosage,
            PrescribingDoctor = request.PrescribingDoctor,
            PrescriptionDate = request.PrescriptionDate,
            Purpose = request.Purpose,
            Instructions = request.Instructions,
            SideEffects = request.SideEffects,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Medications.Add(medication);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Medication created: {MedicationId}", medication.MedicationId);

        return medication.ToDto();
    }
}
