// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Query to get all trends for a user.
/// </summary>
public record GetTrendsByUserIdQuery : IRequest<IEnumerable<TrendDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetTrendsByUserIdQuery.
/// </summary>
public class GetTrendsByUserIdQueryHandler : IRequestHandler<GetTrendsByUserIdQuery, IEnumerable<TrendDto>>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<GetTrendsByUserIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTrendsByUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetTrendsByUserIdQueryHandler(
        IBloodPressureMonitorContext context,
        ILogger<GetTrendsByUserIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TrendDto>> Handle(GetTrendsByUserIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all trends for user {UserId}",
            request.UserId);

        var trends = await _context.Trends
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return trends.Select(x => x.ToDto());
    }
}
