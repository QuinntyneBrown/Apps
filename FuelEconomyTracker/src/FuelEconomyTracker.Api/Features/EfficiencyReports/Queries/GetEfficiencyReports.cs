// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.EfficiencyReports.Queries;

/// <summary>
/// Query to get all efficiency reports.
/// </summary>
public class GetEfficiencyReports : IRequest<List<EfficiencyReportDto>>
{
    public Guid? VehicleId { get; set; }
}

/// <summary>
/// Handler for GetEfficiencyReports query.
/// </summary>
public class GetEfficiencyReportsHandler : IRequestHandler<GetEfficiencyReports, List<EfficiencyReportDto>>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetEfficiencyReportsHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<EfficiencyReportDto>> Handle(GetEfficiencyReports request, CancellationToken cancellationToken)
    {
        var query = _context.EfficiencyReports.AsQueryable();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(r => r.VehicleId == request.VehicleId.Value);
        }

        var reports = await query
            .OrderByDescending(r => r.EndDate)
            .ToListAsync(cancellationToken);

        return reports.Select(r => new EfficiencyReportDto
        {
            EfficiencyReportId = r.EfficiencyReportId,
            VehicleId = r.VehicleId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalMiles = r.TotalMiles,
            TotalGallons = r.TotalGallons,
            AverageMPG = r.AverageMPG,
            TotalFuelCost = r.TotalFuelCost,
            CostPerMile = r.CostPerMile,
            NumberOfFillUps = r.NumberOfFillUps,
            BestMPG = r.BestMPG,
            WorstMPG = r.WorstMPG,
            Notes = r.Notes
        }).ToList();
    }
}
