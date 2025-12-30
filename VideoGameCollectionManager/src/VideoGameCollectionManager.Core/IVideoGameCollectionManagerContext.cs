// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace VideoGameCollectionManager.Core;

public interface IVideoGameCollectionManagerContext
{
    DbSet<Game> Games { get; set; }
    DbSet<PlaySession> PlaySessions { get; set; }
    DbSet<Wishlist> Wishlists { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
