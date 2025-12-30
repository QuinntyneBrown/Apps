using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Tools;

public record UpdateToolCommand : IRequest<ToolDto?>
{
    public Guid ToolId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public string? Description { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
}

public class UpdateToolCommandHandler : IRequestHandler<UpdateToolCommand, ToolDto?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<UpdateToolCommandHandler> _logger;

    public UpdateToolCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<UpdateToolCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ToolDto?> Handle(UpdateToolCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating tool {ToolId}", request.ToolId);

        var tool = await _context.Tools
            .FirstOrDefaultAsync(t => t.ToolId == request.ToolId, cancellationToken);

        if (tool == null)
        {
            _logger.LogWarning("Tool {ToolId} not found", request.ToolId);
            return null;
        }

        tool.Name = request.Name;
        tool.Brand = request.Brand;
        tool.Model = request.Model;
        tool.Description = request.Description;
        tool.PurchasePrice = request.PurchasePrice;
        tool.PurchaseDate = request.PurchaseDate;
        tool.Location = request.Location;
        tool.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated tool {ToolId}", request.ToolId);

        return tool.ToDto();
    }
}
