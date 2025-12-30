using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Gears;

public record UpdateGearCommand : IRequest<GearDto?>
{
    public Guid GearId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string GearType { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public decimal? PurchasePrice { get; init; }
    public string? Notes { get; init; }
}

public class UpdateGearCommandHandler : IRequestHandler<UpdateGearCommand, GearDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<UpdateGearCommandHandler> _logger;

    public UpdateGearCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<UpdateGearCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GearDto?> Handle(UpdateGearCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating gear {GearId}", request.GearId);

        var gear = await _context.Gears
            .FirstOrDefaultAsync(g => g.GearId == request.GearId, cancellationToken);

        if (gear == null)
        {
            _logger.LogWarning("Gear {GearId} not found", request.GearId);
            return null;
        }

        gear.Name = request.Name;
        gear.GearType = request.GearType;
        gear.Brand = request.Brand;
        gear.Model = request.Model;
        gear.PurchaseDate = request.PurchaseDate;
        gear.PurchasePrice = request.PurchasePrice;
        gear.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated gear {GearId}", request.GearId);

        return gear.ToDto();
    }
}
