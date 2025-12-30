// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WineCellarInventory.Core;

public class DrinkingWindow
{
    public Guid DrinkingWindowId { get; set; }
    public Guid UserId { get; set; }
    public Guid WineId { get; set; }
    public Wine? Wine { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
