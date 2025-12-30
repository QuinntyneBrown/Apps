// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Command to update a blood pressure reading.
/// </summary>
public record UpdateReadingCommand : IRequest<ReadingDto?>
{
    /// <summary>
    /// Gets or sets the reading ID.
    /// </summary>
    public Guid ReadingId { get; init; }

    /// <summary>
    /// Gets or sets the systolic pressure.
    /// </summary>
    public int Systolic { get; init; }

    /// <summary>
    /// Gets or sets the diastolic pressure.
    /// </summary>
    public int Diastolic { get; init; }

    /// <summary>
    /// Gets or sets the pulse rate.
    /// </summary>
    public int? Pulse { get; init; }

    /// <summary>
    /// Gets or sets the timestamp when the reading was taken.
    /// </summary>
    public DateTime MeasuredAt { get; init; }

    /// <summary>
    /// Gets or sets the position when measured.
    /// </summary>
    public string? Position { get; init; }

    /// <summary>
    /// Gets or sets the arm used.
    /// </summary>
    public string? Arm { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateReadingCommand.
/// </summary>
public class UpdateReadingCommandHandler : IRequestHandler<UpdateReadingCommand, ReadingDto?>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<UpdateReadingCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateReadingCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateReadingCommandHandler(
        IBloodPressureMonitorContext context,
        ILogger<UpdateReadingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ReadingDto?> Handle(UpdateReadingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating reading {ReadingId}", request.ReadingId);

        var reading = await _context.Readings
            .FirstOrDefaultAsync(x => x.ReadingId == request.ReadingId, cancellationToken);

        if (reading == null)
        {
            _logger.LogWarning("Reading {ReadingId} not found", request.ReadingId);
            return null;
        }

        reading.Systolic = request.Systolic;
        reading.Diastolic = request.Diastolic;
        reading.Pulse = request.Pulse;
        reading.MeasuredAt = request.MeasuredAt;
        reading.Position = request.Position;
        reading.Arm = request.Arm;
        reading.Notes = request.Notes;
        reading.Category = reading.DetermineCategory();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated reading {ReadingId}, Category: {Category}",
            reading.ReadingId,
            reading.Category);

        return reading.ToDto();
    }
}
