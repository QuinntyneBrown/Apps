// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents the status of a warranty.
/// </summary>
public enum WarrantyStatus
{
    /// <summary>
    /// Warranty is active and valid.
    /// </summary>
    Active = 0,

    /// <summary>
    /// Warranty has expired.
    /// </summary>
    Expired = 1,

    /// <summary>
    /// A warranty claim has been filed.
    /// </summary>
    ClaimFiled = 2,

    /// <summary>
    /// Warranty claim was approved.
    /// </summary>
    ClaimApproved = 3,

    /// <summary>
    /// Warranty claim was rejected.
    /// </summary>
    ClaimRejected = 4,

    /// <summary>
    /// Warranty was voided or cancelled.
    /// </summary>
    Voided = 5,
}
