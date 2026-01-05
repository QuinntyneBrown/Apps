// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the format of a receipt.
/// </summary>
public enum ReceiptFormat
{
    /// <summary>
    /// Physical paper receipt.
    /// </summary>
    Paper = 0,

    /// <summary>
    /// PDF document.
    /// </summary>
    Pdf = 1,

    /// <summary>
    /// Scanned image (JPEG, PNG, etc.).
    /// </summary>
    Image = 2,

    /// <summary>
    /// Email receipt.
    /// </summary>
    Email = 3,

    /// <summary>
    /// Digital receipt from app or website.
    /// </summary>
    Digital = 4,
}
