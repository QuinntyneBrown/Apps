// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core;

public record WorkLoggedEvent
{
    public Guid WorkLogId { get; init; }
    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public int HoursWorked { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
