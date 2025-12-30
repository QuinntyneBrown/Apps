// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace GiftIdeaTracker.Core;

public interface IGiftIdeaTrackerContext
{
    DbSet<GiftIdea> GiftIdeas { get; set; }
    DbSet<Recipient> Recipients { get; set; }
    DbSet<Purchase> Purchases { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
