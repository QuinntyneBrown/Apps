// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.FillUps.Queries;

/// <summary>
/// Query to get a fill-up by ID.
/// </summary>
public class GetFillUpById : IRequest<FillUpDto?>
{
    public Guid FillUpId { get; set; }
}

/// <summary>
/// Handler for GetFillUpById query.
/// </summary>
public class GetFillUpByIdHandler : IRequestHandler<GetFillUpById, FillUpDto?>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetFillUpByIdHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<FillUpDto?> Handle(GetFillUpById request, CancellationToken cancellationToken)
    {
        var fillUp = await _context.FillUps
            .FirstOrDefaultAsync(f => f.FillUpId == request.FillUpId, cancellationToken);

        if (fillUp == null)
        {
            return null;
        }

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
