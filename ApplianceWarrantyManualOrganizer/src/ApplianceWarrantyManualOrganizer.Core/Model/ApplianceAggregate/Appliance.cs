// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core;

public class Appliance
{
    public Guid ApplianceId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ApplianceType ApplianceType { get; set; }
    public string? Brand { get; set; }
    public string? ModelNumber { get; set; }
    public string? SerialNumber { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
    public ICollection<Manual> Manuals { get; set; } = new List<Manual>();
    public ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
}
