// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to update an existing modification.
/// </summary>
public record UpdateModificationCommand : IRequest<ModificationDto?>
{
    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }

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
/// Handler for UpdateModificationCommand.
/// </summary>
public class UpdateModificationCommandHandler : IRequestHandler<UpdateModificationCommand, ModificationDto?>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<UpdateModificationCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateModificationCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateModificationCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<UpdateModificationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ModificationDto?> Handle(UpdateModificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating modification {ModificationId}", request.ModificationId);

        var modification = await _context.Modifications
            .FirstOrDefaultAsync(m => m.ModificationId == request.ModificationId, cancellationToken);

        if (modification == null)
        {
            _logger.LogWarning("Modification {ModificationId} not found", request.ModificationId);
            return null;
        }

        modification.Name = request.Name;
        modification.Category = request.Category;
        modification.Description = request.Description;
        modification.Manufacturer = request.Manufacturer;
        modification.EstimatedCost = request.EstimatedCost;
        modification.DifficultyLevel = request.DifficultyLevel;
        modification.EstimatedInstallationTime = request.EstimatedInstallationTime;
        modification.PerformanceGain = request.PerformanceGain;
        modification.CompatibleVehicles = request.CompatibleVehicles ?? new List<string>();
        modification.RequiredTools = request.RequiredTools ?? new List<string>();
        modification.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated modification {ModificationId}", request.ModificationId);

        return modification.ToDto();
    }
}
