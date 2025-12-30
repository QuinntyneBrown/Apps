// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Represents the type of mortgage.
/// </summary>
public enum MortgageType
{
    /// <summary>
    /// Fixed-rate mortgage.
    /// </summary>
    Fixed = 0,

    /// <summary>
    /// Adjustable-rate mortgage (ARM).
    /// </summary>
    ARM = 1,

    /// <summary>
    /// FHA loan.
    /// </summary>
    FHA = 2,

    /// <summary>
    /// VA loan.
    /// </summary>
    VA = 3,

    /// <summary>
    /// USDA loan.
    /// </summary>
    USDA = 4,
}
