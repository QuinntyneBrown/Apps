using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Gears;

public record CreateGearCommand : IRequest<GearDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string GearType { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public decimal? PurchasePrice { get; init; }
    public string? Notes { get; init; }
}

public class CreateGearCommandHandler : IRequestHandler<CreateGearCommand, GearDto>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<CreateGearCommandHandler> _logger;

    public CreateGearCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<CreateGearCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GearDto> Handle(CreateGearCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating gear for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var gear = new Gear
        {
            GearId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            GearType = request.GearType,
            Brand = request.Brand,
            Model = request.Model,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Gears.Add(gear);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created gear {GearId} for user {UserId}",
            gear.GearId,
            request.UserId);

        return gear.ToDto();
    }
}
