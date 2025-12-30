// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Represents the cooking method used in a recipe.
/// </summary>
public enum CookingMethod
{
    /// <summary>
    /// Direct grilling method.
    /// </summary>
    DirectGrilling = 0,

    /// <summary>
    /// Indirect grilling method.
    /// </summary>
    IndirectGrilling = 1,

    /// <summary>
    /// Smoking method.
    /// </summary>
    Smoking = 2,

    /// <summary>
    /// Rotisserie method.
    /// </summary>
    Rotisserie = 3,

    /// <summary>
    /// Searing method.
    /// </summary>
    Searing = 4,

    /// <summary>
    /// Slow and low method.
    /// </summary>
    SlowAndLow = 5,

    /// <summary>
    /// Combination of methods.
    /// </summary>
    Combination = 6,
}
