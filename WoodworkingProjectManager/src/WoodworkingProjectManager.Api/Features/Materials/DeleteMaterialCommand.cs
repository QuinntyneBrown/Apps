using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Materials;

public record DeleteMaterialCommand : IRequest<bool>
{
    public Guid MaterialId { get; init; }
}

public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, bool>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<DeleteMaterialCommandHandler> _logger;

    public DeleteMaterialCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<DeleteMaterialCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting material {MaterialId}", request.MaterialId);

        var material = await _context.Materials
            .FirstOrDefaultAsync(m => m.MaterialId == request.MaterialId, cancellationToken);

        if (material == null)
        {
            _logger.LogWarning("Material {MaterialId} not found", request.MaterialId);
            return false;
        }

        _context.Materials.Remove(material);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted material {MaterialId}", request.MaterialId);

        return true;
    }
}
