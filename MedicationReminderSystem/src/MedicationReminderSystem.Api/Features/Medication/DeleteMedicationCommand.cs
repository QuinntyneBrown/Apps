// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Medication;

/// <summary>
/// Command to delete a medication.
/// </summary>
public record DeleteMedicationCommand(Guid MedicationId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteMedicationCommand.
/// </summary>
public class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand, Unit>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<DeleteMedicationCommandHandler> _logger;

    public DeleteMedicationCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<DeleteMedicationCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting medication: {MedicationId}", request.MedicationId);

        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == request.MedicationId, cancellationToken);

        if (medication == null)
        {
            throw new InvalidOperationException($"Medication with ID {request.MedicationId} not found.");
        }

        _context.Medications.Remove(medication);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Medication deleted: {MedicationId}", request.MedicationId);

        return Unit.Value;
    }
}
