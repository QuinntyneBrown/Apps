// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Vehicles.Commands;

/// <summary>
/// Command to update an existing vehicle.
/// </summary>
public class UpdateVehicle : IRequest<VehicleDto>
{
    public Guid VehicleId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public decimal? TankCapacity { get; set; }
    public decimal? EPACityMPG { get; set; }
    public decimal? EPAHighwayMPG { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// Handler for UpdateVehicle command.
/// </summary>
public class UpdateVehicleHandler : IRequestHandler<UpdateVehicle, VehicleDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public UpdateVehicleHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<VehicleDto> Handle(UpdateVehicle request, CancellationToken cancellationToken)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {request.VehicleId} not found.");
        }

        vehicle.Make = request.Make;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        vehicle.VIN = request.VIN;
        vehicle.LicensePlate = request.LicensePlate;
        vehicle.TankCapacity = request.TankCapacity;
        vehicle.EPACityMPG = request.EPACityMPG;
        vehicle.EPAHighwayMPG = request.EPAHighwayMPG;
        vehicle.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

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
