// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the type of warranty.
/// </summary>
public enum WarrantyType
{
    /// <summary>
    /// Manufacturer's warranty included with the product.
    /// </summary>
    Manufacturer = 0,

    /// <summary>
    /// Extended warranty purchased separately.
    /// </summary>
    Extended = 1,

    /// <summary>
    /// Store or retailer warranty.
    /// </summary>
    Store = 2,

    /// <summary>
    /// Third-party warranty or protection plan.
    /// </summary>
    ThirdParty = 3,

    /// <summary>
    /// Limited warranty with specific coverage.
    /// </summary>
    Limited = 4,

    /// <summary>
    /// Lifetime warranty.
    /// </summary>
    Lifetime = 5,
}
