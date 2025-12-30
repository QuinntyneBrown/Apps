using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Tools;

public record DeleteToolCommand : IRequest<bool>
{
    public Guid ToolId { get; init; }
}

public class DeleteToolCommandHandler : IRequestHandler<DeleteToolCommand, bool>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<DeleteToolCommandHandler> _logger;

    public DeleteToolCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<DeleteToolCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteToolCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tool {ToolId}", request.ToolId);

        var tool = await _context.Tools
            .FirstOrDefaultAsync(t => t.ToolId == request.ToolId, cancellationToken);

        if (tool == null)
        {
            _logger.LogWarning("Tool {ToolId} not found", request.ToolId);
            return false;
        }

        _context.Tools.Remove(tool);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted tool {ToolId}", request.ToolId);

        return true;
    }
}
