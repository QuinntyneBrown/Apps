// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Query to get all recovery exercises for an injury.
/// </summary>
public record GetRecoveryExercisesQuery : IRequest<IEnumerable<RecoveryExerciseDto>>
{
    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }
}

/// <summary>
/// Handler for GetRecoveryExercisesQuery.
/// </summary>
public class GetRecoveryExercisesQueryHandler : IRequestHandler<GetRecoveryExercisesQuery, IEnumerable<RecoveryExerciseDto>>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<GetRecoveryExercisesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRecoveryExercisesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetRecoveryExercisesQueryHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<GetRecoveryExercisesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RecoveryExerciseDto>> Handle(GetRecoveryExercisesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all recovery exercises for injury {InjuryId}",
            request.InjuryId);

        var exercises = await _context.RecoveryExercises
            .AsNoTracking()
            .Where(x => x.InjuryId == request.InjuryId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return exercises.Select(x => x.ToDto());
    }
}
