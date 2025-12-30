// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;

public class ServiceRecordDto
{
    public Guid ServiceRecordId { get; set; }
    public Guid ApplianceId { get; set; }
    public DateTime ServiceDate { get; set; }
    public string? ServiceProvider { get; set; }
    public string? Description { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; }
}
