// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Represents a service or maintenance record for a vehicle.
/// </summary>
public class ServiceRecord
{
    /// <summary>
    /// Gets or sets the unique identifier for the service record.
    /// </summary>
    public Guid ServiceRecordId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the type of service performed.
    /// </summary>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// Gets or sets the date the service was performed.
    /// </summary>
    public DateTime ServiceDate { get; set; }

    /// <summary>
    /// Gets or sets the odometer reading at the time of service.
    /// </summary>
    public decimal MileageAtService { get; set; }

    /// <summary>
    /// Gets or sets the cost of the service.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Gets or sets the service provider name.
    /// </summary>
    public string? ServiceProvider { get; set; }

    /// <summary>
    /// Gets or sets the description of the service performed.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional notes about the service.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the list of parts replaced during service.
    /// </summary>
    public List<string> PartsReplaced { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the receipt or invoice number.
    /// </summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Gets or sets the warranty expiration date.
    /// </summary>
    public DateTime? WarrantyExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Adds parts to the service record.
    /// </summary>
    /// <param name="parts">The parts to add.</param>
    public void AddParts(IEnumerable<string> parts)
    {
        PartsReplaced.AddRange(parts);
    }

    /// <summary>
    /// Updates the service cost.
    /// </summary>
    /// <param name="cost">The new cost.</param>
    /// <exception cref="ArgumentException">Thrown when cost is negative.</exception>
    public void UpdateCost(decimal cost)
    {
        if (cost < 0)
        {
            throw new ArgumentException("Cost cannot be negative.", nameof(cost));
        }

        Cost = cost;
    }

    /// <summary>
    /// Sets the warranty expiration date.
    /// </summary>
    /// <param name="expirationDate">The warranty expiration date.</param>
    public void SetWarrantyExpiration(DateTime expirationDate)
    {
        WarrantyExpirationDate = expirationDate;
    }
}
