// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core;

/// <summary>
/// Represents the billing cycle of a subscription.
/// </summary>
public enum BillingCycle
{
    /// <summary>
    /// Weekly billing.
    /// </summary>
    Weekly = 0,

    /// <summary>
    /// Monthly billing.
    /// </summary>
    Monthly = 1,

    /// <summary>
    /// Quarterly billing.
    /// </summary>
    Quarterly = 2,

    /// <summary>
    /// Annual billing.
    /// </summary>
    Annual = 3,
}
