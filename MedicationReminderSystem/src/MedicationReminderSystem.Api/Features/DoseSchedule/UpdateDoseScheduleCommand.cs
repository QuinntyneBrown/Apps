// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.DoseSchedule;

/// <summary>
/// Command to update an existing dose schedule.
/// </summary>
public record UpdateDoseScheduleCommand : IRequest<DoseScheduleDto>
{
    public Guid DoseScheduleId { get; init; }
    public TimeSpan ScheduledTime { get; init; }
    public string DaysOfWeek { get; init; } = string.Empty;
    public string Frequency { get; init; } = string.Empty;
    public bool ReminderEnabled { get; init; }
    public int ReminderOffsetMinutes { get; init; }
    public bool IsActive { get; init; }
}

/// <summary>
/// Handler for UpdateDoseScheduleCommand.
/// </summary>
public class UpdateDoseScheduleCommandHandler : IRequestHandler<UpdateDoseScheduleCommand, DoseScheduleDto>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<UpdateDoseScheduleCommandHandler> _logger;

    public UpdateDoseScheduleCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<UpdateDoseScheduleCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DoseScheduleDto> Handle(UpdateDoseScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating dose schedule: {DoseScheduleId}", request.DoseScheduleId);

        var doseSchedule = await _context.DoseSchedules
            .FirstOrDefaultAsync(d => d.DoseScheduleId == request.DoseScheduleId, cancellationToken);

        if (doseSchedule == null)
        {
            throw new InvalidOperationException($"Dose schedule with ID {request.DoseScheduleId} not found.");
        }

        doseSchedule.ScheduledTime = request.ScheduledTime;
        doseSchedule.DaysOfWeek = request.DaysOfWeek;
        doseSchedule.Frequency = request.Frequency;
        doseSchedule.ReminderEnabled = request.ReminderEnabled;
        doseSchedule.ReminderOffsetMinutes = request.ReminderOffsetMinutes;
        doseSchedule.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Dose schedule updated: {DoseScheduleId}", doseSchedule.DoseScheduleId);

        return doseSchedule.ToDto();
    }
}
