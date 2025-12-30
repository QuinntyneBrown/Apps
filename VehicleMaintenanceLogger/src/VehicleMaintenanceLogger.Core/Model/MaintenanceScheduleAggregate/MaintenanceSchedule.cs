// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Represents a scheduled maintenance task for a vehicle.
/// </summary>
public class MaintenanceSchedule
{
    /// <summary>
    /// Gets or sets the unique identifier for the maintenance schedule.
    /// </summary>
    public Guid MaintenanceScheduleId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the type of scheduled service.
    /// </summary>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// Gets or sets the description of the scheduled maintenance.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the interval in miles between services.
    /// </summary>
    public decimal? MileageInterval { get; set; }

    /// <summary>
    /// Gets or sets the interval in months between services.
    /// </summary>
    public int? MonthsInterval { get; set; }

    /// <summary>
    /// Gets or sets the mileage of the last service.
    /// </summary>
    public decimal? LastServiceMileage { get; set; }

    /// <summary>
    /// Gets or sets the date of the last service.
    /// </summary>
    public DateTime? LastServiceDate { get; set; }

    /// <summary>
    /// Gets or sets the next scheduled service mileage.
    /// </summary>
    public decimal? NextServiceMileage { get; set; }

    /// <summary>
    /// Gets or sets the next scheduled service date.
    /// </summary>
    public DateTime? NextServiceDate { get; set; }

    /// <summary>
    /// Gets or sets whether the schedule is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets additional notes about the schedule.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Updates the schedule after a service is performed.
    /// </summary>
    /// <param name="serviceMileage">The mileage at which the service was performed.</param>
    /// <param name="serviceDate">The date the service was performed.</param>
    public void RecordService(decimal serviceMileage, DateTime serviceDate)
    {
        LastServiceMileage = serviceMileage;
        LastServiceDate = serviceDate;

        if (MileageInterval.HasValue)
        {
            NextServiceMileage = serviceMileage + MileageInterval.Value;
        }

        if (MonthsInterval.HasValue)
        {
            NextServiceDate = serviceDate.AddMonths(MonthsInterval.Value);
        }
    }

    /// <summary>
    /// Determines if the maintenance is due based on current mileage and date.
    /// </summary>
    /// <param name="currentMileage">The current vehicle mileage.</param>
    /// <param name="currentDate">The current date.</param>
    /// <returns>True if maintenance is due, otherwise false.</returns>
    public bool IsDue(decimal currentMileage, DateTime currentDate)
    {
        if (!IsActive)
        {
            return false;
        }

        bool isDueByMileage = NextServiceMileage.HasValue && currentMileage >= NextServiceMileage.Value;
        bool isDueByDate = NextServiceDate.HasValue && currentDate >= NextServiceDate.Value;

        return isDueByMileage || isDueByDate;
    }

    /// <summary>
    /// Deactivates the schedule.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Reactivates the schedule.
    /// </summary>
    public void Reactivate()
    {
        IsActive = true;
    }
}
