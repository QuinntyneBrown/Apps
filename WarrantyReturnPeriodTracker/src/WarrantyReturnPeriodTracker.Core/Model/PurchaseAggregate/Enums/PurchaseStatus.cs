// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the status of a purchase.
/// </summary>
public enum PurchaseStatus
{
    /// <summary>
    /// Purchase is active and product is owned.
    /// </summary>
    Active = 0,

    /// <summary>
    /// Product was returned to the store.
    /// </summary>
    Returned = 1,

    /// <summary>
    /// Product was disposed of or no longer owned.
    /// </summary>
    Disposed = 2,

    /// <summary>
    /// Purchase is under warranty claim or repair.
    /// </summary>
    UnderWarrantyClaim = 3,
}
