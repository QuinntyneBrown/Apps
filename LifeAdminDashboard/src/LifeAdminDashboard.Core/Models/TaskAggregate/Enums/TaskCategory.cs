// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Represents the category of an administrative task.
/// </summary>
public enum TaskCategory
{
    /// <summary>
    /// Financial tasks (bills, taxes, etc.).
    /// </summary>
    Financial = 0,

    /// <summary>
    /// Health-related tasks (appointments, prescriptions).
    /// </summary>
    Health = 1,

    /// <summary>
    /// Home maintenance tasks.
    /// </summary>
    HomeMaintenance = 2,

    /// <summary>
    /// Vehicle-related tasks.
    /// </summary>
    Vehicle = 3,

    /// <summary>
    /// Insurance-related tasks.
    /// </summary>
    Insurance = 4,

    /// <summary>
    /// Legal or government tasks.
    /// </summary>
    Legal = 5,

    /// <summary>
    /// Subscription management.
    /// </summary>
    Subscriptions = 6,

    /// <summary>
    /// Personal documents.
    /// </summary>
    Documents = 7,

    /// <summary>
    /// Other administrative tasks.
    /// </summary>
    Other = 8,
}
