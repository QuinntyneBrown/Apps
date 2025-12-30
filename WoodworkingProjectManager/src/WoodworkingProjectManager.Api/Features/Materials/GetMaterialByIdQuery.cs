using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Materials;

public record GetMaterialByIdQuery : IRequest<MaterialDto?>
{
    public Guid MaterialId { get; init; }
}

public class GetMaterialByIdQueryHandler : IRequestHandler<GetMaterialByIdQuery, MaterialDto?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<GetMaterialByIdQueryHandler> _logger;

    public GetMaterialByIdQueryHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<GetMaterialByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaterialDto?> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting material {MaterialId}", request.MaterialId);

        var material = await _context.Materials
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MaterialId == request.MaterialId, cancellationToken);

        if (material == null)
        {
            _logger.LogWarning("Material {MaterialId} not found", request.MaterialId);
            return null;
        }

        return material.ToDto();
    }
}
