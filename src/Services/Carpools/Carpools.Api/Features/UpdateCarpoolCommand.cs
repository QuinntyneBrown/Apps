using Carpools.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Carpools.Api.Features;

public record UpdateCarpoolCommand(
    Guid CarpoolId,
    string DriverName,
    string? DriverPhone,
    int AvailableSeats,
    string? PickupLocation,
    TimeSpan? PickupTime,
    string? Notes,
    bool IsConfirmed) : IRequest<CarpoolDto?>;

public class UpdateCarpoolCommandHandler : IRequestHandler<UpdateCarpoolCommand, CarpoolDto?>
{
    private readonly ICarpoolsDbContext _context;
    private readonly ILogger<UpdateCarpoolCommandHandler> _logger;

    public UpdateCarpoolCommandHandler(ICarpoolsDbContext context, ILogger<UpdateCarpoolCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CarpoolDto?> Handle(UpdateCarpoolCommand request, CancellationToken cancellationToken)
    {
        var carpool = await _context.Carpools
            .FirstOrDefaultAsync(c => c.CarpoolId == request.CarpoolId, cancellationToken);

        if (carpool == null) return null;

        carpool.DriverName = request.DriverName;
        carpool.DriverPhone = request.DriverPhone;
        carpool.AvailableSeats = request.AvailableSeats;
        carpool.PickupLocation = request.PickupLocation;
        carpool.PickupTime = request.PickupTime;
        carpool.Notes = request.Notes;
        carpool.IsConfirmed = request.IsConfirmed;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Carpool updated: {CarpoolId}", carpool.CarpoolId);

        return carpool.ToDto();
    }
}
