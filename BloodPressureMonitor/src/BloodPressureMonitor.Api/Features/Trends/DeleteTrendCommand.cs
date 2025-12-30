// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Command to delete a blood pressure trend.
/// </summary>
public record DeleteTrendCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the trend ID.
    /// </summary>
    public Guid TrendId { get; init; }
}

/// <summary>
/// Handler for DeleteTrendCommand.
/// </summary>
public class DeleteTrendCommandHandler : IRequestHandler<DeleteTrendCommand, bool>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<DeleteTrendCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTrendCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteTrendCommandHandler(
        IBloodPressureMonitorContext context,
        ILogger<DeleteTrendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteTrendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting trend {TrendId}", request.TrendId);

        var trend = await _context.Trends
            .FirstOrDefaultAsync(x => x.TrendId == request.TrendId, cancellationToken);

        if (trend == null)
        {
            _logger.LogWarning("Trend {TrendId} not found", request.TrendId);
            return false;
        }

        _context.Trends.Remove(trend);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted trend {TrendId}", request.TrendId);

        return true;
    }
}
