// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to update an existing recovery exercise.
/// </summary>
public record UpdateRecoveryExerciseCommand : IRequest<RecoveryExerciseDto?>
{
    /// <summary>
    /// Gets or sets the recovery exercise ID.
    /// </summary>
    public Guid RecoveryExerciseId { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the frequency.
    /// </summary>
    public string Frequency { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the sets and reps.
    /// </summary>
    public string? SetsAndReps { get; init; }

    /// <summary>
    /// Gets or sets the duration minutes.
    /// </summary>
    public int? DurationMinutes { get; init; }

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string? Instructions { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the exercise is active.
    /// </summary>
    public bool IsActive { get; init; }
}

/// <summary>
/// Handler for UpdateRecoveryExerciseCommand.
/// </summary>
public class UpdateRecoveryExerciseCommandHandler : IRequestHandler<UpdateRecoveryExerciseCommand, RecoveryExerciseDto?>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<UpdateRecoveryExerciseCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRecoveryExerciseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateRecoveryExerciseCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<UpdateRecoveryExerciseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<RecoveryExerciseDto?> Handle(UpdateRecoveryExerciseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating recovery exercise {RecoveryExerciseId}",
            request.RecoveryExerciseId);

        var exercise = await _context.RecoveryExercises
            .FirstOrDefaultAsync(x => x.RecoveryExerciseId == request.RecoveryExerciseId, cancellationToken);

        if (exercise == null)
        {
            _logger.LogWarning(
                "Recovery exercise {RecoveryExerciseId} not found",
                request.RecoveryExerciseId);
            return null;
        }

        exercise.Name = request.Name;
        exercise.Description = request.Description;
        exercise.Frequency = request.Frequency;
        exercise.SetsAndReps = request.SetsAndReps;
        exercise.DurationMinutes = request.DurationMinutes;
        exercise.Instructions = request.Instructions;
        exercise.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated recovery exercise {RecoveryExerciseId}",
            request.RecoveryExerciseId);

        return exercise.ToDto();
    }
}
