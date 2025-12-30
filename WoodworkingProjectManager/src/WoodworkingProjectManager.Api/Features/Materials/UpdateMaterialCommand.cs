using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Materials;

public record UpdateMaterialCommand : IRequest<MaterialDto?>
{
    public Guid MaterialId { get; init; }
    public Guid? ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal? Cost { get; init; }
    public string? Supplier { get; init; }
}

public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, MaterialDto?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<UpdateMaterialCommandHandler> _logger;

    public UpdateMaterialCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<UpdateMaterialCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaterialDto?> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating material {MaterialId}", request.MaterialId);

        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.MaterialId == request.MaterialId, cancellationToken);

        if (material == null)
        {
            _logger.LogWarning("Material {MaterialId} not found", request.MaterialId);
            return null;
        }

        material.ProjectId = request.ProjectId;
        material.Name = request.Name;
        material.Description = request.Description;
        material.Quantity = request.Quantity;
        material.Unit = request.Unit;
        material.Cost = request.Cost;
        material.Supplier = request.Supplier;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated material {MaterialId}", request.MaterialId);

        return material.ToDto();
    }
}
