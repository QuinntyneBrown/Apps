// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core;

/// <summary>
/// Represents the status of a subscription.
/// </summary>
public enum SubscriptionStatus
{
    /// <summary>
    /// Subscription is active.
    /// </summary>
    Active = 0,

    /// <summary>
    /// Subscription is paused.
    /// </summary>
    Paused = 1,

    /// <summary>
    /// Subscription is cancelled.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Subscription is pending activation.
    /// </summary>
    Pending = 3,
}
