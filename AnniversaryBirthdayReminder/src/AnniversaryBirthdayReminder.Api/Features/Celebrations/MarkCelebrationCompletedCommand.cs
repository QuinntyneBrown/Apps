// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to mark a celebration as completed.
/// </summary>
public record MarkCelebrationCompletedCommand : IRequest<CelebrationDto>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the celebration date.
    /// </summary>
    public DateTime CelebrationDate { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the attendees.
    /// </summary>
    public List<string> Attendees { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int? Rating { get; init; }
}

/// <summary>
/// Handler for MarkCelebrationCompletedCommand.
/// </summary>
public class MarkCelebrationCompletedCommandHandler : IRequestHandler<MarkCelebrationCompletedCommand, CelebrationDto>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<MarkCelebrationCompletedCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkCelebrationCompletedCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public MarkCelebrationCompletedCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<MarkCelebrationCompletedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<CelebrationDto> Handle(MarkCelebrationCompletedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Marking celebration as completed for important date {ImportantDateId}",
            request.ImportantDateId);

        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = request.ImportantDateId,
            CelebrationDate = request.CelebrationDate,
            Status = CelebrationStatus.Completed,
        };

        celebration.MarkAsCompleted(request.Notes, request.Rating);
        celebration.AddAttendees(request.Attendees);

        _context.Celebrations.Add(celebration);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created completed celebration {CelebrationId} for important date {ImportantDateId}",
            celebration.CelebrationId,
            request.ImportantDateId);

        return celebration.ToDto();
    }
}
