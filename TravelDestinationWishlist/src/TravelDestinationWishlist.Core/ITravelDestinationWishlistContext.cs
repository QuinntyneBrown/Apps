// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace TravelDestinationWishlist.Core;

public interface ITravelDestinationWishlistContext
{
    DbSet<Destination> Destinations { get; set; }
    DbSet<Trip> Trips { get; set; }
    DbSet<Memory> Memories { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
