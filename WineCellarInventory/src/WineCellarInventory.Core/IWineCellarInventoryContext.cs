// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace WineCellarInventory.Core;

public interface IWineCellarInventoryContext
{
    DbSet<Wine> Wines { get; set; }
    DbSet<TastingNote> TastingNotes { get; set; }
    DbSet<DrinkingWindow> DrinkingWindows { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
