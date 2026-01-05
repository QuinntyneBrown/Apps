// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the status of a receipt.
/// </summary>
public enum ReceiptStatus
{
    /// <summary>
    /// Receipt is active and accessible.
    /// </summary>
    Active = 0,

    /// <summary>
    /// Receipt has been archived.
    /// </summary>
    Archived = 1,

    /// <summary>
    /// Receipt is lost or missing.
    /// </summary>
    Lost = 2,

    /// <summary>
    /// Receipt is invalid or rejected.
    /// </summary>
    Invalid = 3,
}
