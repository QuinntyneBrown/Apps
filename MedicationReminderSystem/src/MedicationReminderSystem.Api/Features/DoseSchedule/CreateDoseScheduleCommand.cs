// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.DoseSchedule;

/// <summary>
/// Command to create a new dose schedule.
/// </summary>
public record CreateDoseScheduleCommand : IRequest<DoseScheduleDto>
{
    public Guid UserId { get; init; }
    public Guid MedicationId { get; init; }
    public TimeSpan ScheduledTime { get; init; }
    public string DaysOfWeek { get; init; } = string.Empty;
    public string Frequency { get; init; } = string.Empty;
    public bool ReminderEnabled { get; init; }
    public int ReminderOffsetMinutes { get; init; }
}

/// <summary>
/// Handler for CreateDoseScheduleCommand.
/// </summary>
public class CreateDoseScheduleCommandHandler : IRequestHandler<CreateDoseScheduleCommand, DoseScheduleDto>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<CreateDoseScheduleCommandHandler> _logger;

    public CreateDoseScheduleCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<CreateDoseScheduleCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DoseScheduleDto> Handle(CreateDoseScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating dose schedule for medication: {MedicationId}", request.MedicationId);

        var doseSchedule = new Core.DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = request.UserId,
            MedicationId = request.MedicationId,
            ScheduledTime = request.ScheduledTime,
            DaysOfWeek = request.DaysOfWeek,
            Frequency = request.Frequency,
            ReminderEnabled = request.ReminderEnabled,
            ReminderOffsetMinutes = request.ReminderOffsetMinutes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.DoseSchedules.Add(doseSchedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Dose schedule created: {DoseScheduleId}", doseSchedule.DoseScheduleId);

        return doseSchedule.ToDto();
    }
}
