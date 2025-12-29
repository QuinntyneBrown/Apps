// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to mark a celebration as skipped.
/// </summary>
public record MarkCelebrationSkippedCommand : IRequest<CelebrationDto>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the skipped date.
    /// </summary>
    public DateTime SkippedDate { get; init; }

    /// <summary>
    /// Gets or sets the reason for skipping.
    /// </summary>
    public string? Reason { get; init; }
}

/// <summary>
/// Handler for MarkCelebrationSkippedCommand.
/// </summary>
public class MarkCelebrationSkippedCommandHandler : IRequestHandler<MarkCelebrationSkippedCommand, CelebrationDto>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<MarkCelebrationSkippedCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkCelebrationSkippedCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public MarkCelebrationSkippedCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<MarkCelebrationSkippedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<CelebrationDto> Handle(MarkCelebrationSkippedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Marking celebration as skipped for important date {ImportantDateId}",
            request.ImportantDateId);

        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = request.ImportantDateId,
            CelebrationDate = request.SkippedDate,
        };

        celebration.MarkAsSkipped(request.Reason);

        _context.Celebrations.Add(celebration);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created skipped celebration {CelebrationId} for important date {ImportantDateId}",
            celebration.CelebrationId,
            request.ImportantDateId);

        return celebration.ToDto();
    }
}
