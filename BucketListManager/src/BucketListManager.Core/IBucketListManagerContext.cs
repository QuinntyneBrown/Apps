// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace BucketListManager.Core;

public interface IBucketListManagerContext
{
    DbSet<BucketListItem> BucketListItems { get; set; }
    DbSet<Milestone> Milestones { get; set; }
    DbSet<Memory> Memories { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
