// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Core;

public interface IClassicCarRestorationLogContext
{
    DbSet<Project> Projects { get; set; }
    DbSet<Part> Parts { get; set; }
    DbSet<WorkLog> WorkLogs { get; set; }
    DbSet<PhotoLog> PhotoLogs { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
