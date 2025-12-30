// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MusicCollectionOrganizer.Core;

public interface IMusicCollectionOrganizerContext
{
    DbSet<Album> Albums { get; set; }
    DbSet<Artist> Artists { get; set; }
    DbSet<ListeningLog> ListeningLogs { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
