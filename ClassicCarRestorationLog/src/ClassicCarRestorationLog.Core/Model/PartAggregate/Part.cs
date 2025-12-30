// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core;

public class Part
{
    public Guid PartId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PartNumber { get; set; }
    public string? Supplier { get; set; }
    public decimal? Cost { get; set; }
    public DateTime? OrderedDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public bool IsInstalled { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
