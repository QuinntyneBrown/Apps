// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Query to get all readings for a user.
/// </summary>
public record GetReadingsByUserIdQuery : IRequest<IEnumerable<ReadingDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the start date for filtering.
    /// </summary>
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date for filtering.
    /// </summary>
    public DateTime? EndDate { get; init; }
}

/// <summary>
/// Handler for GetReadingsByUserIdQuery.
/// </summary>
public class GetReadingsByUserIdQueryHandler : IRequestHandler<GetReadingsByUserIdQuery, IEnumerable<ReadingDto>>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<GetReadingsByUserIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetReadingsByUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetReadingsByUserIdQueryHandler(
        IBloodPressureMonitorContext context,
        ILogger<GetReadingsByUserIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ReadingDto>> Handle(GetReadingsByUserIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all readings for user {UserId}",
            request.UserId);

        var query = _context.Readings
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId);

        if (request.StartDate.HasValue)
        {
            query = query.Where(x => x.MeasuredAt >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(x => x.MeasuredAt <= request.EndDate.Value);
        }

        var readings = await query
            .OrderByDescending(x => x.MeasuredAt)
            .ToListAsync(cancellationToken);

        return readings.Select(x => x.ToDto());
    }
}
