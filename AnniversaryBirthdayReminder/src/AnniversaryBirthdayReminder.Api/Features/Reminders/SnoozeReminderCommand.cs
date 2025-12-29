// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to snooze a reminder.
/// </summary>
public record SnoozeReminderCommand : IRequest<ReminderDto?>
{
    /// <summary>
    /// Gets or sets the reminder ID.
    /// </summary>
    public Guid ReminderId { get; init; }

    /// <summary>
    /// Gets or sets the snooze duration in minutes.
    /// </summary>
    public int SnoozeMinutes { get; init; } = 60;
}

/// <summary>
/// Handler for SnoozeReminderCommand.
/// </summary>
public class SnoozeReminderCommandHandler : IRequestHandler<SnoozeReminderCommand, ReminderDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<SnoozeReminderCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SnoozeReminderCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public SnoozeReminderCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<SnoozeReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ReminderDto?> Handle(SnoozeReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Snoozing reminder {ReminderId} for {SnoozeMinutes} minutes",
            request.ReminderId,
            request.SnoozeMinutes);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning(
                "Reminder {ReminderId} not found",
                request.ReminderId);
            return null;
        }

        reminder.Snooze(TimeSpan.FromMinutes(request.SnoozeMinutes));
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Snoozed reminder {ReminderId} until {ScheduledTime}",
            request.ReminderId,
            reminder.ScheduledTime);

        return reminder.ToDto();
    }
}
