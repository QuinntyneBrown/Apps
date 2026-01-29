using Vehicles.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Vehicles.Api.Features;

public record GetVehicleByIdQuery(Guid VehicleId) : IRequest<VehicleDto?>;

public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDto?>
{
    private readonly IVehiclesDbContext _context;
    public GetVehicleByIdQueryHandler(IVehiclesDbContext context) => _context = context;
    public async Task<VehicleDto?> Handle(GetVehicleByIdQuery request, CancellationToken ct)
    {
        var vehicle = await _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, ct);
        return vehicle?.ToDto();
    }
}
