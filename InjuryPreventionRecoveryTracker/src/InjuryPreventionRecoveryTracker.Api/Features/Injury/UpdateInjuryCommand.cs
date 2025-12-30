// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to update an existing injury.
/// </summary>
public record UpdateInjuryCommand : IRequest<InjuryDto?>
{
    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets or sets the injury type.
    /// </summary>
    public InjuryType InjuryType { get; init; }

    /// <summary>
    /// Gets or sets the injury severity.
    /// </summary>
    public InjurySeverity Severity { get; init; }

    /// <summary>
    /// Gets or sets the body part affected.
    /// </summary>
    public string BodyPart { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the injury occurred.
    /// </summary>
    public DateTime InjuryDate { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the diagnosis.
    /// </summary>
    public string? Diagnosis { get; init; }

    /// <summary>
    /// Gets or sets the expected recovery days.
    /// </summary>
    public int? ExpectedRecoveryDays { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the progress percentage.
    /// </summary>
    public int ProgressPercentage { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateInjuryCommand.
/// </summary>
public class UpdateInjuryCommandHandler : IRequestHandler<UpdateInjuryCommand, InjuryDto?>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<UpdateInjuryCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateInjuryCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateInjuryCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<UpdateInjuryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<InjuryDto?> Handle(UpdateInjuryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating injury {InjuryId}",
            request.InjuryId);

        var injury = await _context.Injuries
            .FirstOrDefaultAsync(x => x.InjuryId == request.InjuryId, cancellationToken);

        if (injury == null)
        {
            _logger.LogWarning(
                "Injury {InjuryId} not found",
                request.InjuryId);
            return null;
        }

        injury.InjuryType = request.InjuryType;
        injury.Severity = request.Severity;
        injury.BodyPart = request.BodyPart;
        injury.InjuryDate = request.InjuryDate;
        injury.Description = request.Description;
        injury.Diagnosis = request.Diagnosis;
        injury.ExpectedRecoveryDays = request.ExpectedRecoveryDays;
        injury.Status = request.Status;
        injury.ProgressPercentage = request.ProgressPercentage;
        injury.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated injury {InjuryId}",
            request.InjuryId);

        return injury.ToDto();
    }
}
