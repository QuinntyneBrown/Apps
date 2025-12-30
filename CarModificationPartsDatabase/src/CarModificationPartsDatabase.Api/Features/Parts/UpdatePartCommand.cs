// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to update an existing part.
/// </summary>
public record UpdatePartCommand : IRequest<PartDto?>
{
    /// <summary>
    /// Gets or sets the part ID.
    /// </summary>
    public Guid PartId { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the part number or SKU.
    /// </summary>
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the manufacturer or brand.
    /// </summary>
    public string Manufacturer { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the part.
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Gets or sets the category of the part.
    /// </summary>
    public ModCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the list of compatible vehicle models.
    /// </summary>
    public List<string>? CompatibleVehicles { get; init; }

    /// <summary>
    /// Gets or sets the warranty information.
    /// </summary>
    public string? WarrantyInfo { get; init; }

    /// <summary>
    /// Gets or sets the weight of the part in pounds.
    /// </summary>
    public decimal? Weight { get; init; }

    /// <summary>
    /// Gets or sets the dimensions of the part.
    /// </summary>
    public string? Dimensions { get; init; }

    /// <summary>
    /// Gets or sets whether the part is currently in stock.
    /// </summary>
    public bool InStock { get; init; }

    /// <summary>
    /// Gets or sets the supplier or vendor name.
    /// </summary>
    public string? Supplier { get; init; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdatePartCommand.
/// </summary>
public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand, PartDto?>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<UpdatePartCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePartCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdatePartCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<UpdatePartCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PartDto?> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating part {PartId}", request.PartId);

        var part = await _context.Parts
            .FirstOrDefaultAsync(p => p.PartId == request.PartId, cancellationToken);

        if (part == null)
        {
            _logger.LogWarning("Part {PartId} not found", request.PartId);
            return null;
        }

        part.Name = request.Name;
        part.PartNumber = request.PartNumber;
        part.Manufacturer = request.Manufacturer;
        part.Description = request.Description;
        part.Price = request.Price;
        part.Category = request.Category;
        part.CompatibleVehicles = request.CompatibleVehicles ?? new List<string>();
        part.WarrantyInfo = request.WarrantyInfo;
        part.Weight = request.Weight;
        part.Dimensions = request.Dimensions;
        part.InStock = request.InStock;
        part.Supplier = request.Supplier;
        part.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated part {PartId}", request.PartId);

        return part.ToDto();
    }
}
