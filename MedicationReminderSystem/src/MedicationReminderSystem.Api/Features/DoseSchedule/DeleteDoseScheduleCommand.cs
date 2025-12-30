// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.DoseSchedule;

/// <summary>
/// Command to delete a dose schedule.
/// </summary>
public record DeleteDoseScheduleCommand(Guid DoseScheduleId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteDoseScheduleCommand.
/// </summary>
public class DeleteDoseScheduleCommandHandler : IRequestHandler<DeleteDoseScheduleCommand, Unit>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<DeleteDoseScheduleCommandHandler> _logger;

    public DeleteDoseScheduleCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<DeleteDoseScheduleCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteDoseScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting dose schedule: {DoseScheduleId}", request.DoseScheduleId);

        var doseSchedule = await _context.DoseSchedules
            .FirstOrDefaultAsync(d => d.DoseScheduleId == request.DoseScheduleId, cancellationToken);

        if (doseSchedule == null)
        {
            throw new InvalidOperationException($"Dose schedule with ID {request.DoseScheduleId} not found.");
        }

        _context.DoseSchedules.Remove(doseSchedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Dose schedule deleted: {DoseScheduleId}", request.DoseScheduleId);

        return Unit.Value;
    }
}
