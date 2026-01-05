// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Represents a fuel efficiency report for a specific time period.
/// </summary>
public class EfficiencyReport
{
    /// <summary>
    /// Gets or sets the unique identifier for the efficiency report.
    /// </summary>
    public Guid EfficiencyReportId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the report period start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the report period end date.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the total miles driven in this period.
    /// </summary>
    public decimal TotalMiles { get; set; }

    /// <summary>
    /// Gets or sets the total gallons consumed in this period.
    /// </summary>
    public decimal TotalGallons { get; set; }

    /// <summary>
    /// Gets or sets the average MPG for this period.
    /// </summary>
    public decimal AverageMPG { get; set; }

    /// <summary>
    /// Gets or sets the total fuel cost for this period.
    /// </summary>
    public decimal TotalFuelCost { get; set; }

    /// <summary>
    /// Gets or sets the cost per mile.
    /// </summary>
    public decimal CostPerMile { get; set; }

    /// <summary>
    /// Gets or sets the number of fill-ups in this period.
    /// </summary>
    public int NumberOfFillUps { get; set; }

    /// <summary>
    /// Gets or sets the best MPG recorded in this period.
    /// </summary>
    public decimal? BestMPG { get; set; }

    /// <summary>
    /// Gets or sets the worst MPG recorded in this period.
    /// </summary>
    public decimal? WorstMPG { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the report.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Calculates the average MPG.
    /// </summary>
    public void CalculateAverageMPG()
    {
        if (TotalGallons > 0)
        {
            AverageMPG = Math.Round(TotalMiles / TotalGallons, 2);
        }
    }

    /// <summary>
    /// Calculates the cost per mile.
    /// </summary>
    public void CalculateCostPerMile()
    {
        if (TotalMiles > 0)
        {
            CostPerMile = Math.Round(TotalFuelCost / TotalMiles, 4);
        }
    }

    /// <summary>
    /// Generates the report based on fill-up data.
    /// </summary>
    /// <param name="fillUps">The fill-ups to include in the report.</param>
    public void GenerateReport(IEnumerable<FillUp> fillUps)
    {
        var fillUpList = fillUps.ToList();
        NumberOfFillUps = fillUpList.Count;
        TotalGallons = fillUpList.Sum(f => f.Gallons);
        TotalFuelCost = fillUpList.Sum(f => f.TotalCost);

        var fillUpsWithMPG = fillUpList.Where(f => f.MilesPerGallon.HasValue).ToList();
        if (fillUpsWithMPG.Any())
        {
            BestMPG = fillUpsWithMPG.Max(f => f.MilesPerGallon!.Value);
            WorstMPG = fillUpsWithMPG.Min(f => f.MilesPerGallon!.Value);
        }

        CalculateAverageMPG();
        CalculateCostPerMile();
    }
}
