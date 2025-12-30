// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;

namespace PersonalNetWorthDashboard.Api.Features.Asset;

public record AssetDto(
    Guid AssetId,
    string Name,
    AssetType AssetType,
    decimal CurrentValue,
    decimal? PurchasePrice,
    DateTime? PurchaseDate,
    string? Institution,
    string? AccountNumber,
    string? Notes,
    DateTime LastUpdated,
    bool IsActive);

public static class AssetExtensions
{
    public static AssetDto ToDto(this Core.Asset asset)
    {
        return new AssetDto(
            asset.AssetId,
            asset.Name,
            asset.AssetType,
            asset.CurrentValue,
            asset.PurchasePrice,
            asset.PurchaseDate,
            asset.Institution,
            asset.AccountNumber,
            asset.Notes,
            asset.LastUpdated,
            asset.IsActive);
    }
}
