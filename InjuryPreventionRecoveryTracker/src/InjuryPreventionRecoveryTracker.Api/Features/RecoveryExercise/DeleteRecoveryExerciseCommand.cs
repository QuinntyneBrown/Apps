// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to delete a recovery exercise.
/// </summary>
public record DeleteRecoveryExerciseCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the recovery exercise ID.
    /// </summary>
    public Guid RecoveryExerciseId { get; init; }
}

/// <summary>
/// Handler for DeleteRecoveryExerciseCommand.
/// </summary>
public class DeleteRecoveryExerciseCommandHandler : IRequestHandler<DeleteRecoveryExerciseCommand, bool>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<DeleteRecoveryExerciseCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRecoveryExerciseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteRecoveryExerciseCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<DeleteRecoveryExerciseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteRecoveryExerciseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting recovery exercise {RecoveryExerciseId}",
            request.RecoveryExerciseId);

        var exercise = await _context.RecoveryExercises
            .FirstOrDefaultAsync(x => x.RecoveryExerciseId == request.RecoveryExerciseId, cancellationToken);

        if (exercise == null)
        {
            _logger.LogWarning(
                "Recovery exercise {RecoveryExerciseId} not found",
                request.RecoveryExerciseId);
            return false;
        }

        _context.RecoveryExercises.Remove(exercise);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted recovery exercise {RecoveryExerciseId}",
            request.RecoveryExerciseId);

        return true;
    }
}
