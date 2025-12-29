// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to schedule a reminder.
/// </summary>
public record ScheduleReminderCommand : IRequest<ReminderDto?>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the advance notice days.
    /// </summary>
    public int AdvanceNoticeDays { get; init; }

    /// <summary>
    /// Gets or sets the delivery channel.
    /// </summary>
    public DeliveryChannel DeliveryChannel { get; init; }
}

/// <summary>
/// Handler for ScheduleReminderCommand.
/// </summary>
public class ScheduleReminderCommandHandler : IRequestHandler<ScheduleReminderCommand, ReminderDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<ScheduleReminderCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleReminderCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public ScheduleReminderCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<ScheduleReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ReminderDto?> Handle(ScheduleReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Scheduling reminder for important date {ImportantDateId}",
            request.ImportantDateId);

        var importantDate = await _context.ImportantDates
            .FirstOrDefaultAsync(x => x.ImportantDateId == request.ImportantDateId, cancellationToken);

        if (importantDate == null)
        {
            _logger.LogWarning(
                "Important date {ImportantDateId} not found",
                request.ImportantDateId);
            return null;
        }

        var nextOccurrence = importantDate.GetNextOccurrence();
        var scheduledTime = nextOccurrence.AddDays(-request.AdvanceNoticeDays);

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = request.ImportantDateId,
            ScheduledTime = scheduledTime,
            AdvanceNoticeDays = request.AdvanceNoticeDays,
            DeliveryChannel = request.DeliveryChannel,
            Status = ReminderStatus.Scheduled,
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Scheduled reminder {ReminderId} for {ScheduledTime}",
            reminder.ReminderId,
            scheduledTime);

        return reminder.ToDto();
    }
}
