// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the type of receipt.
/// </summary>
public enum ReceiptType
{
    /// <summary>
    /// Original purchase receipt.
    /// </summary>
    Purchase = 0,

    /// <summary>
    /// Return receipt.
    /// </summary>
    Return = 1,

    /// <summary>
    /// Exchange receipt.
    /// </summary>
    Exchange = 2,

    /// <summary>
    /// Warranty registration receipt.
    /// </summary>
    WarrantyRegistration = 3,

    /// <summary>
    /// Refund receipt.
    /// </summary>
    Refund = 4,
}
