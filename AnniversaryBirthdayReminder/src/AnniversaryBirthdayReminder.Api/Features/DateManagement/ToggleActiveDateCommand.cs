// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to toggle the active status of an important date.
/// </summary>
public record ToggleActiveDateCommand : IRequest<ImportantDateDto?>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }
}

/// <summary>
/// Handler for ToggleActiveDateCommand.
/// </summary>
public class ToggleActiveDateCommandHandler : IRequestHandler<ToggleActiveDateCommand, ImportantDateDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<ToggleActiveDateCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ToggleActiveDateCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public ToggleActiveDateCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<ToggleActiveDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ImportantDateDto?> Handle(ToggleActiveDateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Toggling active status for important date {ImportantDateId}",
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

        importantDate.ToggleActive();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled active status for important date {ImportantDateId} to {IsActive}",
            request.ImportantDateId,
            importantDate.IsActive);

        return importantDate.ToDto();
    }
}
