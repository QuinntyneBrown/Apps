using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Tools;

public record CreateToolCommand : IRequest<ToolDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public string? Description { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
}

public class CreateToolCommandHandler : IRequestHandler<CreateToolCommand, ToolDto>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<CreateToolCommandHandler> _logger;

    public CreateToolCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<CreateToolCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ToolDto> Handle(CreateToolCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating tool for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Brand = request.Brand,
            Model = request.Model,
            Description = request.Description,
            PurchasePrice = request.PurchasePrice,
            PurchaseDate = request.PurchaseDate,
            Location = request.Location,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Tools.Add(tool);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created tool {ToolId} for user {UserId}",
            tool.ToolId,
            request.UserId);

        return tool.ToDto();
    }
}
