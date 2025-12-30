// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to update an existing installation.
/// </summary>
public record UpdateInstallationCommand : IRequest<InstallationDto?>
{
    /// <summary>
    /// Gets or sets the installation ID.
    /// </summary>
    public Guid InstallationId { get; init; }

    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }

    /// <summary>
    /// Gets or sets the vehicle information.
    /// </summary>
    public string VehicleInfo { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the installation date.
    /// </summary>
    public DateTime InstallationDate { get; init; }

    /// <summary>
    /// Gets or sets the installer name (shop or individual).
    /// </summary>
    public string? InstalledBy { get; init; }

    /// <summary>
    /// Gets or sets the actual installation cost.
    /// </summary>
    public decimal? InstallationCost { get; init; }

    /// <summary>
    /// Gets or sets the parts cost.
    /// </summary>
    public decimal? PartsCost { get; init; }

    /// <summary>
    /// Gets or sets the labor hours.
    /// </summary>
    public decimal? LaborHours { get; init; }

    /// <summary>
    /// Gets or sets the list of parts used in the installation.
    /// </summary>
    public List<string>? PartsUsed { get; init; }

    /// <summary>
    /// Gets or sets the installation notes or observations.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the difficulty rating (1-5).
    /// </summary>
    public int? DifficultyRating { get; init; }

    /// <summary>
    /// Gets or sets the satisfaction rating (1-5).
    /// </summary>
    public int? SatisfactionRating { get; init; }

    /// <summary>
    /// Gets or sets the list of photos URLs.
    /// </summary>
    public List<string>? Photos { get; init; }

    /// <summary>
    /// Gets or sets whether this installation is completed.
    /// </summary>
    public bool IsCompleted { get; init; }
}

/// <summary>
/// Handler for UpdateInstallationCommand.
/// </summary>
public class UpdateInstallationCommandHandler : IRequestHandler<UpdateInstallationCommand, InstallationDto?>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<UpdateInstallationCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateInstallationCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateInstallationCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<UpdateInstallationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<InstallationDto?> Handle(UpdateInstallationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating installation {InstallationId}", request.InstallationId);

        var installation = await _context.Installations
            .FirstOrDefaultAsync(i => i.InstallationId == request.InstallationId, cancellationToken);

        if (installation == null)
        {
            _logger.LogWarning("Installation {InstallationId} not found", request.InstallationId);
            return null;
        }

        installation.ModificationId = request.ModificationId;
        installation.VehicleInfo = request.VehicleInfo;
        installation.InstallationDate = request.InstallationDate;
        installation.InstalledBy = request.InstalledBy;
        installation.InstallationCost = request.InstallationCost;
        installation.PartsCost = request.PartsCost;
        installation.LaborHours = request.LaborHours;
        installation.PartsUsed = request.PartsUsed ?? new List<string>();
        installation.Notes = request.Notes;
        installation.DifficultyRating = request.DifficultyRating;
        installation.SatisfactionRating = request.SatisfactionRating;
        installation.Photos = request.Photos ?? new List<string>();
        installation.IsCompleted = request.IsCompleted;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated installation {InstallationId}", request.InstallationId);

        return installation.ToDto();
    }
}
