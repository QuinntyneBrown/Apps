using Vehicles.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Vehicles.Api.Features;

public record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;

public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
{
    private readonly IVehiclesDbContext _context;
    public GetVehiclesQueryHandler(IVehiclesDbContext context) => _context = context;
    public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken ct) => await _context.Vehicles.AsNoTracking().Select(v => v.ToDto()).ToListAsync(ct);
}
