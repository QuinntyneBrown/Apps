// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Represents the type of an asset.
/// </summary>
public enum AssetType
{
    /// <summary>
    /// Cash in bank accounts.
    /// </summary>
    Cash = 0,

    /// <summary>
    /// Investment account (stocks, bonds, mutual funds).
    /// </summary>
    Investment = 1,

    /// <summary>
    /// Retirement account (401k, IRA, etc.).
    /// </summary>
    Retirement = 2,

    /// <summary>
    /// Real estate property.
    /// </summary>
    RealEstate = 3,

    /// <summary>
    /// Vehicle (car, boat, etc.).
    /// </summary>
    Vehicle = 4,

    /// <summary>
    /// Personal property or collectibles.
    /// </summary>
    PersonalProperty = 5,

    /// <summary>
    /// Business ownership or equity.
    /// </summary>
    Business = 6,

    /// <summary>
    /// Other type of asset.
    /// </summary>
    Other = 7,
}
