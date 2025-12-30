// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to create a new recovery exercise.
/// </summary>
public record CreateRecoveryExerciseCommand : IRequest<RecoveryExerciseDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

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
}

/// <summary>
/// Handler for CreateRecoveryExerciseCommand.
/// </summary>
public class CreateRecoveryExerciseCommandHandler : IRequestHandler<CreateRecoveryExerciseCommand, RecoveryExerciseDto>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<CreateRecoveryExerciseCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRecoveryExerciseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateRecoveryExerciseCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<CreateRecoveryExerciseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<RecoveryExerciseDto> Handle(CreateRecoveryExerciseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating recovery exercise for injury {InjuryId}, name {Name}",
            request.InjuryId,
            request.Name);

        var exercise = new RecoveryExercise
        {
            RecoveryExerciseId = Guid.NewGuid(),
            UserId = request.UserId,
            InjuryId = request.InjuryId,
            Name = request.Name,
            Description = request.Description,
            Frequency = request.Frequency,
            SetsAndReps = request.SetsAndReps,
            DurationMinutes = request.DurationMinutes,
            Instructions = request.Instructions,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.RecoveryExercises.Add(exercise);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created recovery exercise {RecoveryExerciseId} for injury {InjuryId}",
            exercise.RecoveryExerciseId,
            request.InjuryId);

        return exercise.ToDto();
    }
}
