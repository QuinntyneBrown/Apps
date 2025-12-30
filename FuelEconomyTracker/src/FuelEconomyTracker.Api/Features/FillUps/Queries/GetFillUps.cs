// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.FillUps.Queries;

/// <summary>
/// Query to get all fill-ups.
/// </summary>
public class GetFillUps : IRequest<List<FillUpDto>>
{
    public Guid? VehicleId { get; set; }
}

/// <summary>
/// Handler for GetFillUps query.
/// </summary>
public class GetFillUpsHandler : IRequestHandler<GetFillUps, List<FillUpDto>>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetFillUpsHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<FillUpDto>> Handle(GetFillUps request, CancellationToken cancellationToken)
    {
        var query = _context.FillUps.AsQueryable();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(f => f.VehicleId == request.VehicleId.Value);
        }

        var fillUps = await query
            .OrderByDescending(f => f.FillUpDate)
            .ToListAsync(cancellationToken);

        return fillUps.Select(f => new FillUpDto
        {
            FillUpId = f.FillUpId,
            VehicleId = f.VehicleId,
            FillUpDate = f.FillUpDate,
            Odometer = f.Odometer,
            Gallons = f.Gallons,
            PricePerGallon = f.PricePerGallon,
            TotalCost = f.TotalCost,
            IsFullTank = f.IsFullTank,
            FuelGrade = f.FuelGrade,
            GasStation = f.GasStation,
            MilesPerGallon = f.MilesPerGallon,
            Notes = f.Notes
        }).ToList();
    }
}
