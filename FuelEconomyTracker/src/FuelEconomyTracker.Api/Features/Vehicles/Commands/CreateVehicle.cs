// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Vehicles.Commands;

/// <summary>
/// Command to create a new vehicle.
/// </summary>
public class CreateVehicle : IRequest<VehicleDto>
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public decimal? TankCapacity { get; set; }
    public decimal? EPACityMPG { get; set; }
    public decimal? EPAHighwayMPG { get; set; }
}

/// <summary>
/// Handler for CreateVehicle command.
/// </summary>
public class CreateVehicleHandler : IRequestHandler<CreateVehicle, VehicleDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public CreateVehicleHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<VehicleDto> Handle(CreateVehicle request, CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle
        {
            VehicleId = Guid.NewGuid(),
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            VIN = request.VIN,
            LicensePlate = request.LicensePlate,
            TankCapacity = request.TankCapacity,
            EPACityMPG = request.EPACityMPG,
            EPAHighwayMPG = request.EPAHighwayMPG,
            IsActive = true
        };

        _context.Vehicles.Add(vehicle);
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
