// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Command to create a new material.
/// </summary>
public record CreateMaterialCommand : IRequest<MaterialDto>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the material name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public int Quantity { get; init; }

    /// <summary>
    /// Gets or sets the unit.
    /// </summary>
    public string? Unit { get; init; }

    /// <summary>
    /// Gets or sets the unit cost.
    /// </summary>
    public decimal? UnitCost { get; init; }

    /// <summary>
    /// Gets or sets the total cost.
    /// </summary>
    public decimal? TotalCost { get; init; }

    /// <summary>
    /// Gets or sets the supplier.
    /// </summary>
    public string? Supplier { get; init; }
}

/// <summary>
/// Handler for CreateMaterialCommand.
/// </summary>
public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, MaterialDto>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<CreateMaterialCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMaterialCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateMaterialCommandHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<CreateMaterialCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<MaterialDto> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating material for project {ProjectId}, name {Name}",
            request.ProjectId,
            request.Name);

        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            Name = request.Name,
            Quantity = request.Quantity,
            Unit = request.Unit,
            UnitCost = request.UnitCost,
            TotalCost = request.TotalCost,
            Supplier = request.Supplier,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Materials.Add(material);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created material {MaterialId} for project {ProjectId}",
            material.MaterialId,
            request.ProjectId);

        return material.ToDto();
    }
}
