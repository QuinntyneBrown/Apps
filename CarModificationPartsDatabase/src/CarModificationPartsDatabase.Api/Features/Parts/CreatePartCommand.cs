// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to create a new part.
/// </summary>
public record CreatePartCommand : IRequest<PartDto>
{
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
    public bool InStock { get; init; } = true;

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
/// Handler for CreatePartCommand.
/// </summary>
public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, PartDto>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<CreatePartCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePartCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreatePartCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<CreatePartCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PartDto> Handle(CreatePartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating part {Name}", request.Name);

        var part = new Part
        {
            PartId = Guid.NewGuid(),
            Name = request.Name,
            PartNumber = request.PartNumber,
            Manufacturer = request.Manufacturer,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            CompatibleVehicles = request.CompatibleVehicles ?? new List<string>(),
            WarrantyInfo = request.WarrantyInfo,
            Weight = request.Weight,
            Dimensions = request.Dimensions,
            InStock = request.InStock,
            Supplier = request.Supplier,
            Notes = request.Notes,
        };

        _context.Parts.Add(part);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created part {PartId}", part.PartId);

        return part.ToDto();
    }
}
