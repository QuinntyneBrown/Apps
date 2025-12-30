// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Query to get an injury by ID.
/// </summary>
public record GetInjuryByIdQuery : IRequest<InjuryDto?>
{
    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }
}

/// <summary>
/// Handler for GetInjuryByIdQuery.
/// </summary>
public class GetInjuryByIdQueryHandler : IRequestHandler<GetInjuryByIdQuery, InjuryDto?>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<GetInjuryByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInjuryByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetInjuryByIdQueryHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<GetInjuryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<InjuryDto?> Handle(GetInjuryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting injury {InjuryId}",
            request.InjuryId);

        var injury = await _context.Injuries
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.InjuryId == request.InjuryId, cancellationToken);

        if (injury == null)
        {
            _logger.LogInformation(
                "Injury {InjuryId} not found",
                request.InjuryId);
            return null;
        }

        return injury.ToDto();
    }
}
