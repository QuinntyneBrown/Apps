// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to update an existing important date.
/// </summary>
public record UpdateImportantDateCommand : IRequest<ImportantDateDto?>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the person name.
    /// </summary>
    public string PersonName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the date type.
    /// </summary>
    public DateType DateType { get; init; }

    /// <summary>
    /// Gets or sets the date value.
    /// </summary>
    public DateTime DateValue { get; init; }

    /// <summary>
    /// Gets or sets the recurrence pattern.
    /// </summary>
    public RecurrencePattern RecurrencePattern { get; init; }

    /// <summary>
    /// Gets or sets the relationship.
    /// </summary>
    public string? Relationship { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateImportantDateCommand.
/// </summary>
public class UpdateImportantDateCommandHandler : IRequestHandler<UpdateImportantDateCommand, ImportantDateDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<UpdateImportantDateCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateImportantDateCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateImportantDateCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<UpdateImportantDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ImportantDateDto?> Handle(UpdateImportantDateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating important date {ImportantDateId}",
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

        importantDate.PersonName = request.PersonName;
        importantDate.DateType = request.DateType;
        importantDate.DateValue = request.DateValue;
        importantDate.RecurrencePattern = request.RecurrencePattern;
        importantDate.Relationship = request.Relationship;
        importantDate.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated important date {ImportantDateId}",
            request.ImportantDateId);

        return importantDate.ToDto();
    }
}
