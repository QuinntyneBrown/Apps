// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to delete an injury.
/// </summary>
public record DeleteInjuryCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }
}

/// <summary>
/// Handler for DeleteInjuryCommand.
/// </summary>
public class DeleteInjuryCommandHandler : IRequestHandler<DeleteInjuryCommand, bool>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<DeleteInjuryCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteInjuryCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteInjuryCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<DeleteInjuryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteInjuryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting injury {InjuryId}",
            request.InjuryId);

        var injury = await _context.Injuries
            .Include(x => x.RecoveryExercises)
            .Include(x => x.Milestones)
            .FirstOrDefaultAsync(x => x.InjuryId == request.InjuryId, cancellationToken);

        if (injury == null)
        {
            _logger.LogWarning(
                "Injury {InjuryId} not found",
                request.InjuryId);
            return false;
        }

        _context.Injuries.Remove(injury);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted injury {InjuryId}",
            request.InjuryId);

        return true;
    }
}
