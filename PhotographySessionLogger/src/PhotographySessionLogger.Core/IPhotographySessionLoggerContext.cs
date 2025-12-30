// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PhotographySessionLogger.Core;

public interface IPhotographySessionLoggerContext
{
    DbSet<Session> Sessions { get; set; }
    DbSet<Photo> Photos { get; set; }
    DbSet<Gear> Gears { get; set; }
    DbSet<Project> Projects { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
