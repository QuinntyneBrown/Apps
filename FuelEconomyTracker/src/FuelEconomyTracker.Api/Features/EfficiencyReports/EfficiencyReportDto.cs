// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Api.Features.EfficiencyReports;

/// <summary>
/// Data transfer object for EfficiencyReport.
/// </summary>
public class EfficiencyReportDto
{
    public Guid EfficiencyReportId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalMiles { get; set; }
    public decimal TotalGallons { get; set; }
    public decimal AverageMPG { get; set; }
    public decimal TotalFuelCost { get; set; }
    public decimal CostPerMile { get; set; }
    public int NumberOfFillUps { get; set; }
    public decimal? BestMPG { get; set; }
    public decimal? WorstMPG { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Request to generate an efficiency report.
/// </summary>
public class GenerateEfficiencyReportRequest
{
    public Guid VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Notes { get; set; }
}
