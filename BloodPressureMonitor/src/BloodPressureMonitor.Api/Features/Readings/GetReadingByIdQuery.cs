// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Query to get a reading by ID.
/// </summary>
public record GetReadingByIdQuery : IRequest<ReadingDto?>
{
    /// <summary>
    /// Gets or sets the reading ID.
    /// </summary>
    public Guid ReadingId { get; init; }
}

/// <summary>
/// Handler for GetReadingByIdQuery.
/// </summary>
public class GetReadingByIdQueryHandler : IRequestHandler<GetReadingByIdQuery, ReadingDto?>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<GetReadingByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetReadingByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetReadingByIdQueryHandler(
        IBloodPressureMonitorContext context,
        ILogger<GetReadingByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ReadingDto?> Handle(GetReadingByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reading {ReadingId}", request.ReadingId);

        var reading = await _context.Readings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReadingId == request.ReadingId, cancellationToken);

        return reading?.ToDto();
    }
}
