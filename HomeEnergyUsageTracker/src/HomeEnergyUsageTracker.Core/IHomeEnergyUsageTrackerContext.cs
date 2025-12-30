// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Core;

public interface IHomeEnergyUsageTrackerContext
{
    DbSet<UtilityBill> UtilityBills { get; set; }
    DbSet<Usage> Usages { get; set; }
    DbSet<SavingsTip> SavingsTips { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
