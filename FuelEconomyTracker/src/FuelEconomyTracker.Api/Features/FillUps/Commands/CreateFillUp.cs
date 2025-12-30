// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.FillUps.Commands;

/// <summary>
/// Command to create a new fill-up.
/// </summary>
public class CreateFillUp : IRequest<FillUpDto>
{
    public Guid VehicleId { get; set; }
    public DateTime FillUpDate { get; set; }
    public decimal Odometer { get; set; }
    public decimal Gallons { get; set; }
    public decimal PricePerGallon { get; set; }
    public bool IsFullTank { get; set; }
    public string? FuelGrade { get; set; }
    public string? GasStation { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateFillUp command.
/// </summary>
public class CreateFillUpHandler : IRequestHandler<CreateFillUp, FillUpDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public CreateFillUpHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<FillUpDto> Handle(CreateFillUp request, CancellationToken cancellationToken)
    {
        var fillUp = new FillUp
        {
            FillUpId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            FillUpDate = request.FillUpDate,
            Odometer = request.Odometer,
            Gallons = request.Gallons,
            PricePerGallon = request.PricePerGallon,
            IsFullTank = request.IsFullTank,
            FuelGrade = request.FuelGrade,
            GasStation = request.GasStation,
            Notes = request.Notes
        };

        fillUp.CalculateTotalCost();

        // Calculate MPG based on previous fill-up
        var previousFillUp = await _context.FillUps
            .Where(f => f.VehicleId == request.VehicleId && f.FillUpDate < request.FillUpDate)
            .OrderByDescending(f => f.FillUpDate)
            .FirstOrDefaultAsync(cancellationToken);

        if (previousFillUp != null)
        {
            fillUp.CalculateMPG(previousFillUp.Odometer);
        }

        _context.FillUps.Add(fillUp);
        await _context.SaveChangesAsync(cancellationToken);

        return new FillUpDto
        {
            FillUpId = fillUp.FillUpId,
            VehicleId = fillUp.VehicleId,
            FillUpDate = fillUp.FillUpDate,
            Odometer = fillUp.Odometer,
            Gallons = fillUp.Gallons,
            PricePerGallon = fillUp.PricePerGallon,
            TotalCost = fillUp.TotalCost,
            IsFullTank = fillUp.IsFullTank,
            FuelGrade = fillUp.FuelGrade,
            GasStation = fillUp.GasStation,
            MilesPerGallon = fillUp.MilesPerGallon,
            Notes = fillUp.Notes
        };
    }
}
