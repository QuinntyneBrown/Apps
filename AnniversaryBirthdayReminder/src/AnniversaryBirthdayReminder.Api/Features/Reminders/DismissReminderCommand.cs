// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to dismiss a reminder.
/// </summary>
public record DismissReminderCommand : IRequest<ReminderDto?>
{
    /// <summary>
    /// Gets or sets the reminder ID.
    /// </summary>
    public Guid ReminderId { get; init; }
}

/// <summary>
/// Handler for DismissReminderCommand.
/// </summary>
public class DismissReminderCommandHandler : IRequestHandler<DismissReminderCommand, ReminderDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<DismissReminderCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DismissReminderCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DismissReminderCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<DismissReminderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ReminderDto?> Handle(DismissReminderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Dismissing reminder {ReminderId}",
            request.ReminderId);

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            _logger.LogWarning(
                "Reminder {ReminderId} not found",
                request.ReminderId);
            return null;
        }

        reminder.Dismiss();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Dismissed reminder {ReminderId}",
            request.ReminderId);

        return reminder.ToDto();
    }
}
