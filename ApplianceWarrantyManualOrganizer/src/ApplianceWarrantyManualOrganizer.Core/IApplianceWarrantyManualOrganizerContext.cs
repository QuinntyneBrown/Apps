// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Core;

public interface IApplianceWarrantyManualOrganizerContext
{
    DbSet<Appliance> Appliances { get; set; }
    DbSet<Warranty> Warranties { get; set; }
    DbSet<Manual> Manuals { get; set; }
    DbSet<ServiceRecord> ServiceRecords { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
