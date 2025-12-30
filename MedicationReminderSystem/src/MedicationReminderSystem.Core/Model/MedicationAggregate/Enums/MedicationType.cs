// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Represents the type of medication.
/// </summary>
public enum MedicationType
{
    /// <summary>
    /// Tablet or pill form.
    /// </summary>
    Tablet = 0,

    /// <summary>
    /// Capsule form.
    /// </summary>
    Capsule = 1,

    /// <summary>
    /// Liquid form.
    /// </summary>
    Liquid = 2,

    /// <summary>
    /// Injection form.
    /// </summary>
    Injection = 3,

    /// <summary>
    /// Topical cream or ointment.
    /// </summary>
    Topical = 4,

    /// <summary>
    /// Inhaler.
    /// </summary>
    Inhaler = 5,

    /// <summary>
    /// Patch.
    /// </summary>
    Patch = 6,

    /// <summary>
    /// Other or custom type.
    /// </summary>
    Other = 7,
}
