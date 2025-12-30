// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Query to get all injuries for a user.
/// </summary>
public record GetInjuriesQuery : IRequest<IEnumerable<InjuryDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetInjuriesQuery.
/// </summary>
public class GetInjuriesQueryHandler : IRequestHandler<GetInjuriesQuery, IEnumerable<InjuryDto>>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<GetInjuriesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInjuriesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetInjuriesQueryHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<GetInjuriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InjuryDto>> Handle(GetInjuriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all injuries for user {UserId}",
            request.UserId);

        var injuries = await _context.Injuries
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.InjuryDate)
            .ToListAsync(cancellationToken);

        return injuries.Select(x => x.ToDto());
    }
}
