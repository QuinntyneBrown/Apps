// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.FillUps.Commands;

/// <summary>
/// Command to update an existing fill-up.
/// </summary>
public class UpdateFillUp : IRequest<FillUpDto>
{
    public Guid FillUpId { get; set; }
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
/// Handler for UpdateFillUp command.
/// </summary>
public class UpdateFillUpHandler : IRequestHandler<UpdateFillUp, FillUpDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public UpdateFillUpHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<FillUpDto> Handle(UpdateFillUp request, CancellationToken cancellationToken)
    {
        var fillUp = await _context.FillUps
            .FirstOrDefaultAsync(f => f.FillUpId == request.FillUpId, cancellationToken);

        if (fillUp == null)
        {
            throw new KeyNotFoundException($"FillUp with ID {request.FillUpId} not found.");
        }

        fillUp.FillUpDate = request.FillUpDate;
        fillUp.Odometer = request.Odometer;
        fillUp.Gallons = request.Gallons;
        fillUp.PricePerGallon = request.PricePerGallon;
        fillUp.IsFullTank = request.IsFullTank;
        fillUp.FuelGrade = request.FuelGrade;
        fillUp.GasStation = request.GasStation;
        fillUp.Notes = request.Notes;

        fillUp.CalculateTotalCost();

        // Recalculate MPG based on previous fill-up
        var previousFillUp = await _context.FillUps
            .Where(f => f.VehicleId == fillUp.VehicleId && f.FillUpDate < request.FillUpDate && f.FillUpId != request.FillUpId)
            .OrderByDescending(f => f.FillUpDate)
            .FirstOrDefaultAsync(cancellationToken);

        if (previousFillUp != null)
        {
            fillUp.CalculateMPG(previousFillUp.Odometer);
        }

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
