// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.EfficiencyReports.Commands;

/// <summary>
/// Command to generate a new efficiency report.
/// </summary>
public class GenerateEfficiencyReport : IRequest<EfficiencyReportDto>
{
    public Guid VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for GenerateEfficiencyReport command.
/// </summary>
public class GenerateEfficiencyReportHandler : IRequestHandler<GenerateEfficiencyReport, EfficiencyReportDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GenerateEfficiencyReportHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<EfficiencyReportDto> Handle(GenerateEfficiencyReport request, CancellationToken cancellationToken)
    {
        var fillUps = await _context.FillUps
            .Where(f => f.VehicleId == request.VehicleId &&
                       f.FillUpDate >= request.StartDate &&
                       f.FillUpDate <= request.EndDate)
            .ToListAsync(cancellationToken);

        var report = new EfficiencyReport
        {
            EfficiencyReportId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Notes = request.Notes
        };

        // Calculate total miles from fill-ups
        if (fillUps.Any())
        {
            var firstOdometer = fillUps.Min(f => f.Odometer);
            var lastOdometer = fillUps.Max(f => f.Odometer);
            report.TotalMiles = lastOdometer - firstOdometer;
        }

        report.GenerateReport(fillUps);

        _context.EfficiencyReports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);

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
