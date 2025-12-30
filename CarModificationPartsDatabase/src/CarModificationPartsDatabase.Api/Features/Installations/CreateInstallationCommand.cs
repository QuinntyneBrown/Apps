// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to create a new installation.
/// </summary>
public record CreateInstallationCommand : IRequest<InstallationDto>
{
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
    public bool IsCompleted { get; init; } = false;
}

/// <summary>
/// Handler for CreateInstallationCommand.
/// </summary>
public class CreateInstallationCommandHandler : IRequestHandler<CreateInstallationCommand, InstallationDto>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<CreateInstallationCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInstallationCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateInstallationCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<CreateInstallationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<InstallationDto> Handle(CreateInstallationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating installation for modification {ModificationId}", request.ModificationId);

        var installation = new Installation
        {
            InstallationId = Guid.NewGuid(),
            ModificationId = request.ModificationId,
            VehicleInfo = request.VehicleInfo,
            InstallationDate = request.InstallationDate,
            InstalledBy = request.InstalledBy,
            InstallationCost = request.InstallationCost,
            PartsCost = request.PartsCost,
            LaborHours = request.LaborHours,
            PartsUsed = request.PartsUsed ?? new List<string>(),
            Notes = request.Notes,
            DifficultyRating = request.DifficultyRating,
            SatisfactionRating = request.SatisfactionRating,
            Photos = request.Photos ?? new List<string>(),
            IsCompleted = request.IsCompleted,
        };

        _context.Installations.Add(installation);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created installation {InstallationId}", installation.InstallationId);

        return installation.ToDto();
    }
}
