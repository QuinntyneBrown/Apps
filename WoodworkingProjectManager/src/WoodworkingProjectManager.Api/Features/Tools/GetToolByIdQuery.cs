using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Tools;

public record GetToolByIdQuery : IRequest<ToolDto?>
{
    public Guid ToolId { get; init; }
}

public class GetToolByIdQueryHandler : IRequestHandler<GetToolByIdQuery, ToolDto?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<GetToolByIdQueryHandler> _logger;

    public GetToolByIdQueryHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<GetToolByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ToolDto?> Handle(GetToolByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tool {ToolId}", request.ToolId);

        var tool = await _context.Tools
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ToolId == request.ToolId, cancellationToken);

        if (tool == null)
        {
            _logger.LogWarning("Tool {ToolId} not found", request.ToolId);
            return null;
        }

        return tool.ToDto();
    }
}
