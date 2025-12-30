// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Command to create a new blood pressure reading.
/// </summary>
public record CreateReadingCommand : IRequest<ReadingDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

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
    public DateTime? MeasuredAt { get; init; }

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
/// Handler for CreateReadingCommand.
/// </summary>
public class CreateReadingCommandHandler : IRequestHandler<CreateReadingCommand, ReadingDto>
{
    private readonly IBloodPressureMonitorContext _context;
    private readonly ILogger<CreateReadingCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReadingCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateReadingCommandHandler(
        IBloodPressureMonitorContext context,
        ILogger<CreateReadingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ReadingDto> Handle(CreateReadingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating reading for user {UserId}, BP: {Systolic}/{Diastolic}",
            request.UserId,
            request.Systolic,
            request.Diastolic);

        var reading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = request.UserId,
            Systolic = request.Systolic,
            Diastolic = request.Diastolic,
            Pulse = request.Pulse,
            MeasuredAt = request.MeasuredAt ?? DateTime.UtcNow,
            Position = request.Position,
            Arm = request.Arm,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        reading.Category = reading.DetermineCategory();

        _context.Readings.Add(reading);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created reading {ReadingId} for user {UserId}, Category: {Category}",
            reading.ReadingId,
            request.UserId,
            reading.Category);

        return reading.ToDto();
    }
}
