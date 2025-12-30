// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Vehicles.Queries;

/// <summary>
/// Query to get a vehicle by ID.
/// </summary>
public class GetVehicleById : IRequest<VehicleDto?>
{
    public Guid VehicleId { get; set; }
}

/// <summary>
/// Handler for GetVehicleById query.
/// </summary>
public class GetVehicleByIdHandler : IRequestHandler<GetVehicleById, VehicleDto?>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetVehicleByIdHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<VehicleDto?> Handle(GetVehicleById request, CancellationToken cancellationToken)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            return null;
        }

        return new VehicleDto
        {
            VehicleId = vehicle.VehicleId,
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = vehicle.Year,
            VIN = vehicle.VIN,
            LicensePlate = vehicle.LicensePlate,
            TankCapacity = vehicle.TankCapacity,
            EPACityMPG = vehicle.EPACityMPG,
            EPAHighwayMPG = vehicle.EPAHighwayMPG,
            IsActive = vehicle.IsActive
        };
    }
}
