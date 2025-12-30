// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Query to get critical readings for a user.
/// </summary>
public record GetCriticalReadingsQuery : IRequest<IEnumerable<ReadingDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the number of days to look back (default 30).
    /// </summary>
    public int DaysBack { get; init; } = 30;
}

/// <summary>
/// Handler for GetCriticalReadingsQuery.
/// </summary>
public class GetCriticalReadingsQueryHandler : IRequestHandler<GetCriticalReadingsQuery, IEnumerable<ReadingDto>>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<GetCriticalReadingsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCriticalReadingsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetCriticalReadingsQueryHandler(
        IBloodPressureMonitorContext context,
        ILogger<GetCriticalReadingsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ReadingDto>> Handle(GetCriticalReadingsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting critical readings for user {UserId} within {DaysBack} days",
            request.UserId,
            request.DaysBack);

        var cutoffDate = DateTime.UtcNow.AddDays(-request.DaysBack);

        var readings = await _context.Readings
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId &&
                       x.MeasuredAt >= cutoffDate &&
                       x.Category == BloodPressureCategory.HypertensiveCrisis)
            .OrderByDescending(x => x.MeasuredAt)
            .ToListAsync(cancellationToken);

        return readings.Select(x => x.ToDto());
    }
}
