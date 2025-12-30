// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Vehicles.Queries;

/// <summary>
/// Query to get all vehicles.
/// </summary>
public class GetVehicles : IRequest<List<VehicleDto>>
{
    public bool? IsActive { get; set; }
}

/// <summary>
/// Handler for GetVehicles query.
/// </summary>
public class GetVehiclesHandler : IRequestHandler<GetVehicles, List<VehicleDto>>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetVehiclesHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleDto>> Handle(GetVehicles request, CancellationToken cancellationToken)
    {
        var query = _context.Vehicles.AsQueryable();

        if (request.IsActive.HasValue)
        {
            query = query.Where(v => v.IsActive == request.IsActive.Value);
        }

        var vehicles = await query
            .OrderByDescending(v => v.Year)
            .ThenBy(v => v.Make)
            .ThenBy(v => v.Model)
            .ToListAsync(cancellationToken);

        return vehicles.Select(v => new VehicleDto
        {
            VehicleId = v.VehicleId,
            Make = v.Make,
            Model = v.Model,
            Year = v.Year,
            VIN = v.VIN,
            LicensePlate = v.LicensePlate,
            TankCapacity = v.TankCapacity,
            EPACityMPG = v.EPACityMPG,
            EPAHighwayMPG = v.EPAHighwayMPG,
            IsActive = v.IsActive
        }).ToList();
    }
}
