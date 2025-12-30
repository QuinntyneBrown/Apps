// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core;

/// <summary>
/// Represents the status of a brewing batch.
/// </summary>
public enum BatchStatus
{
    /// <summary>
    /// Batch is planned but not started.
    /// </summary>
    Planned = 0,

    /// <summary>
    /// Batch is currently fermenting.
    /// </summary>
    Fermenting = 1,

    /// <summary>
    /// Batch has been bottled.
    /// </summary>
    Bottled = 2,

    /// <summary>
    /// Batch is conditioning.
    /// </summary>
    Conditioning = 3,

    /// <summary>
    /// Batch is completed and ready.
    /// </summary>
    Completed = 4,

    /// <summary>
    /// Batch was contaminated or failed.
    /// </summary>
    Failed = 5,
}
