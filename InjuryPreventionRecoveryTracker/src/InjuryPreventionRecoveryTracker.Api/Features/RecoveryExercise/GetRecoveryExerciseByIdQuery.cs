// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Query to get a recovery exercise by ID.
/// </summary>
public record GetRecoveryExerciseByIdQuery : IRequest<RecoveryExerciseDto?>
{
    /// <summary>
    /// Gets or sets the recovery exercise ID.
    /// </summary>
    public Guid RecoveryExerciseId { get; init; }
}

/// <summary>
/// Handler for GetRecoveryExerciseByIdQuery.
/// </summary>
public class GetRecoveryExerciseByIdQueryHandler : IRequestHandler<GetRecoveryExerciseByIdQuery, RecoveryExerciseDto?>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<GetRecoveryExerciseByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRecoveryExerciseByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetRecoveryExerciseByIdQueryHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<GetRecoveryExerciseByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<RecoveryExerciseDto?> Handle(GetRecoveryExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting recovery exercise {RecoveryExerciseId}",
            request.RecoveryExerciseId);

        var exercise = await _context.RecoveryExercises
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RecoveryExerciseId == request.RecoveryExerciseId, cancellationToken);

        if (exercise == null)
        {
            _logger.LogInformation(
                "Recovery exercise {RecoveryExerciseId} not found",
                request.RecoveryExerciseId);
            return null;
        }

        return exercise.ToDto();
    }
}
