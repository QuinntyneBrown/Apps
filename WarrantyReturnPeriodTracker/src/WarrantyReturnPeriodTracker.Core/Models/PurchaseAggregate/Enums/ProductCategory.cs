// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the category of a purchased product.
/// </summary>
public enum ProductCategory
{
    /// <summary>
    /// Electronics and technology products.
    /// </summary>
    Electronics = 0,

    /// <summary>
    /// Appliances and home equipment.
    /// </summary>
    Appliances = 1,

    /// <summary>
    /// Furniture items.
    /// </summary>
    Furniture = 2,

    /// <summary>
    /// Clothing and apparel.
    /// </summary>
    Clothing = 3,

    /// <summary>
    /// Tools and hardware.
    /// </summary>
    Tools = 4,

    /// <summary>
    /// Automotive parts and accessories.
    /// </summary>
    Automotive = 5,

    /// <summary>
    /// Sporting goods and equipment.
    /// </summary>
    Sports = 6,

    /// <summary>
    /// Other or miscellaneous products.
    /// </summary>
    Other = 7,
}
