using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Tools;

public record GetToolsQuery : IRequest<IEnumerable<ToolDto>>
{
    public Guid? UserId { get; init; }
}

public class GetToolsQueryHandler : IRequestHandler<GetToolsQuery, IEnumerable<ToolDto>>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<GetToolsQueryHandler> _logger;

    public GetToolsQueryHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<GetToolsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ToolDto>> Handle(GetToolsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tools for user {UserId}", request.UserId);

        var query = _context.Tools.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        var tools = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return tools.Select(t => t.ToDto());
    }
}
