// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to create a new important date.
/// </summary>
public record CreateImportantDateCommand : IRequest<ImportantDateDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

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
/// Handler for CreateImportantDateCommand.
/// </summary>
public class CreateImportantDateCommandHandler : IRequestHandler<CreateImportantDateCommand, ImportantDateDto>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<CreateImportantDateCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateImportantDateCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateImportantDateCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<CreateImportantDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ImportantDateDto> Handle(CreateImportantDateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating important date for user {UserId}, person {PersonName}",
            request.UserId,
            request.PersonName);

        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = request.UserId,
            PersonName = request.PersonName,
            DateType = request.DateType,
            DateValue = request.DateValue,
            RecurrencePattern = request.RecurrencePattern,
            Relationship = request.Relationship,
            Notes = request.Notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ImportantDates.Add(importantDate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created important date {ImportantDateId} for user {UserId}",
            importantDate.ImportantDateId,
            request.UserId);

        return importantDate.ToDto();
    }
}
