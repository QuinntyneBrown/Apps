// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Command to delete a blood pressure reading.
/// </summary>
public record DeleteReadingCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the reading ID.
    /// </summary>
    public Guid ReadingId { get; init; }
}

/// <summary>
/// Handler for DeleteReadingCommand.
/// </summary>
public class DeleteReadingCommandHandler : IRequestHandler<DeleteReadingCommand, bool>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<DeleteReadingCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteReadingCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteReadingCommandHandler(
        IBloodPressureMonitorContext context,
        ILogger<DeleteReadingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteReadingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting reading {ReadingId}", request.ReadingId);

        var reading = await _context.Readings
            .FirstOrDefaultAsync(x => x.ReadingId == request.ReadingId, cancellationToken);

        if (reading == null)
        {
            _logger.LogWarning("Reading {ReadingId} not found", request.ReadingId);
            return false;
        }

        _context.Readings.Remove(reading);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted reading {ReadingId}", request.ReadingId);

        return true;
    }
}
