// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to create a new modification.
/// </summary>
public record CreateModificationCommand : IRequest<ModificationDto>
{
    /// <summary>
    /// Gets or sets the modification name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the modification category.
    /// </summary>
    public ModCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the detailed description of the modification.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the manufacturer or brand.
    /// </summary>
    public string? Manufacturer { get; init; }

    /// <summary>
    /// Gets or sets the estimated cost of the modification.
    /// </summary>
    public decimal? EstimatedCost { get; init; }

    /// <summary>
    /// Gets or sets the difficulty level (1-5).
    /// </summary>
    public int? DifficultyLevel { get; init; }

    /// <summary>
    /// Gets or sets the estimated installation time in hours.
    /// </summary>
    public decimal? EstimatedInstallationTime { get; init; }

    /// <summary>
    /// Gets or sets the performance gain description.
    /// </summary>
    public string? PerformanceGain { get; init; }

    /// <summary>
    /// Gets or sets the list of compatible vehicle models.
    /// </summary>
    public List<string>? CompatibleVehicles { get; init; }

    /// <summary>
    /// Gets or sets the list of required tools.
    /// </summary>
    public List<string>? RequiredTools { get; init; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateModificationCommand.
/// </summary>
public class CreateModificationCommandHandler : IRequestHandler<CreateModificationCommand, ModificationDto>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<CreateModificationCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModificationCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateModificationCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<CreateModificationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ModificationDto> Handle(CreateModificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating modification {Name}", request.Name);

        var modification = new Modification
        {
            ModificationId = Guid.NewGuid(),
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            Manufacturer = request.Manufacturer,
            EstimatedCost = request.EstimatedCost,
            DifficultyLevel = request.DifficultyLevel,
            EstimatedInstallationTime = request.EstimatedInstallationTime,
            PerformanceGain = request.PerformanceGain,
            CompatibleVehicles = request.CompatibleVehicles ?? new List<string>(),
            RequiredTools = request.RequiredTools ?? new List<string>(),
            Notes = request.Notes,
        };

        _context.Modifications.Add(modification);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created modification {ModificationId}", modification.ModificationId);

        return modification.ToDto();
    }
}
