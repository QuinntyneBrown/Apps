// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Vehicles.Commands;

/// <summary>
/// Command to delete a vehicle.
/// </summary>
public class DeleteVehicle : IRequest<Unit>
{
    public Guid VehicleId { get; set; }
}

/// <summary>
/// Handler for DeleteVehicle command.
/// </summary>
public class DeleteVehicleHandler : IRequestHandler<DeleteVehicle, Unit>
{
    private readonly IFuelEconomyTrackerContext _context;

    public DeleteVehicleHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteVehicle request, CancellationToken cancellationToken)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            throw new KeyNotFoundException($"Vehicle with ID {request.VehicleId} not found.");
        }

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
