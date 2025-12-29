// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to delete an important date.
/// </summary>
public record DeleteImportantDateCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }
}

/// <summary>
/// Handler for DeleteImportantDateCommand.
/// </summary>
public class DeleteImportantDateCommandHandler : IRequestHandler<DeleteImportantDateCommand, bool>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<DeleteImportantDateCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteImportantDateCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteImportantDateCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<DeleteImportantDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteImportantDateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting important date {ImportantDateId}",
            request.ImportantDateId);

        var importantDate = await _context.ImportantDates
            .Include(x => x.Gifts)
            .FirstOrDefaultAsync(x => x.ImportantDateId == request.ImportantDateId, cancellationToken);

        if (importantDate == null)
        {
            _logger.LogWarning(
                "Important date {ImportantDateId} not found",
                request.ImportantDateId);
            return false;
        }

        if (importantDate.HasPendingGifts())
        {
            _logger.LogWarning(
                "Cannot delete important date {ImportantDateId} with pending gifts",
                request.ImportantDateId);
            return false;
        }

        _context.ImportantDates.Remove(importantDate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted important date {ImportantDateId}",
            request.ImportantDateId);

        return true;
    }
}
