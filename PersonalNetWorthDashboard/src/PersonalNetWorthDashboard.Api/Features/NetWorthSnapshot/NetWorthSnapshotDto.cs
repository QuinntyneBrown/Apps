// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;

namespace PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;

public record NetWorthSnapshotDto(
    Guid NetWorthSnapshotId,
    DateTime SnapshotDate,
    decimal TotalAssets,
    decimal TotalLiabilities,
    decimal NetWorth,
    string? Notes,
    DateTime CreatedAt);

public static class NetWorthSnapshotExtensions
{
    public static NetWorthSnapshotDto ToDto(this Core.NetWorthSnapshot snapshot)
    {
        return new NetWorthSnapshotDto(
            snapshot.NetWorthSnapshotId,
            snapshot.SnapshotDate,
            snapshot.TotalAssets,
            snapshot.TotalLiabilities,
            snapshot.NetWorth,
            snapshot.Notes,
            snapshot.CreatedAt);
    }
}
