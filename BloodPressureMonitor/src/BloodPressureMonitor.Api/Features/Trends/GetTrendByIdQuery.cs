// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Query to get a trend by ID.
/// </summary>
public record GetTrendByIdQuery : IRequest<TrendDto?>
{
    /// <summary>
    /// Gets or sets the trend ID.
    /// </summary>
    public Guid TrendId { get; init; }
}

/// <summary>
/// Handler for GetTrendByIdQuery.
/// </summary>
public class GetTrendByIdQueryHandler : IRequestHandler<GetTrendByIdQuery, TrendDto?>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<GetTrendByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTrendByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetTrendByIdQueryHandler(
        IBloodPressureMonitorContext context,
        ILogger<GetTrendByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<TrendDto?> Handle(GetTrendByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trend {TrendId}", request.TrendId);

        var trend = await _context.Trends
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TrendId == request.TrendId, cancellationToken);

        return trend?.ToDto();
    }
}
