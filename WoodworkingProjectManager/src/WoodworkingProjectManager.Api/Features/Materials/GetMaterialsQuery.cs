using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Materials;

public record GetMaterialsQuery : IRequest<IEnumerable<MaterialDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ProjectId { get; init; }
}

public class GetMaterialsQueryHandler : IRequestHandler<GetMaterialsQuery, IEnumerable<MaterialDto>>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<GetMaterialsQueryHandler> _logger;

    public GetMaterialsQueryHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<GetMaterialsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MaterialDto>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting materials for user {UserId}", request.UserId);

        var query = _context.Materials.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(m => m.UserId == request.UserId.Value);
        }

        if (request.ProjectId.HasValue)
        {
            query = query.Where(m => m.ProjectId == request.ProjectId.Value);
        }

        var materials = await query
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);

        return materials.Select(m => m.ToDto());
    }
}
