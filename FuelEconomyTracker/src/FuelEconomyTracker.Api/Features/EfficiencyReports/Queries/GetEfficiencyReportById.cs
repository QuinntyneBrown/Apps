// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.EfficiencyReports.Queries;

/// <summary>
/// Query to get an efficiency report by ID.
/// </summary>
public class GetEfficiencyReportById : IRequest<EfficiencyReportDto?>
{
    public Guid EfficiencyReportId { get; set; }
}

/// <summary>
/// Handler for GetEfficiencyReportById query.
/// </summary>
public class GetEfficiencyReportByIdHandler : IRequestHandler<GetEfficiencyReportById, EfficiencyReportDto?>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetEfficiencyReportByIdHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<EfficiencyReportDto?> Handle(GetEfficiencyReportById request, CancellationToken cancellationToken)
    {
        var report = await _context.EfficiencyReports
            .FirstOrDefaultAsync(r => r.EfficiencyReportId == request.EfficiencyReportId, cancellationToken);

        if (report == null)
        {
            return null;
        }

        return new EfficiencyReportDto
        {
            EfficiencyReportId = report.EfficiencyReportId,
            VehicleId = report.VehicleId,
            StartDate = report.StartDate,
            EndDate = report.EndDate,
            TotalMiles = report.TotalMiles,
            TotalGallons = report.TotalGallons,
            AverageMPG = report.AverageMPG,
            TotalFuelCost = report.TotalFuelCost,
            CostPerMile = report.CostPerMile,
            NumberOfFillUps = report.NumberOfFillUps,
            BestMPG = report.BestMPG,
            WorstMPG = report.WorstMPG,
            Notes = report.Notes
        };
    }
}
