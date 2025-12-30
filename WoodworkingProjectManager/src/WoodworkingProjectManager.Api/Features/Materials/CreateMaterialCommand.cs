using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Materials;

public record CreateMaterialCommand : IRequest<MaterialDto>
{
    public Guid UserId { get; init; }
    public Guid? ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Quantity { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal? Cost { get; init; }
    public string? Supplier { get; init; }
}

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, MaterialDto>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<CreateMaterialCommandHandler> _logger;

    public CreateMaterialCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<CreateMaterialCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaterialDto> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating material for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = request.UserId,
            ProjectId = request.ProjectId,
            Name = request.Name,
            Description = request.Description,
            Quantity = request.Quantity,
            Unit = request.Unit,
            Cost = request.Cost,
            Supplier = request.Supplier,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Materials.Add(material);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created material {MaterialId} for user {UserId}",
            material.MaterialId,
            request.UserId);

        return material.ToDto();
    }
}
