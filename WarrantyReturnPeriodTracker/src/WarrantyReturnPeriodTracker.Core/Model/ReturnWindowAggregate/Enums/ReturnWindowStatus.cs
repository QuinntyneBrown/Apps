// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the status of a return window.
/// </summary>
public enum ReturnWindowStatus
{
    /// <summary>
    /// Return window is open and product can be returned.
    /// </summary>
    Open = 0,

    /// <summary>
    /// Return window was used (product was returned).
    /// </summary>
    Used = 1,

    /// <summary>
    /// Return window has expired.
    /// </summary>
    Expired = 2,

    /// <summary>
    /// Return window was voided or cancelled.
    /// </summary>
    Voided = 3,
}
