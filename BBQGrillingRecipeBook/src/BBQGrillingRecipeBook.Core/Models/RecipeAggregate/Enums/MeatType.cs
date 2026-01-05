// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Represents the type of meat used in a recipe.
/// </summary>
public enum MeatType
{
    /// <summary>
    /// Beef meat.
    /// </summary>
    Beef = 0,

    /// <summary>
    /// Pork meat.
    /// </summary>
    Pork = 1,

    /// <summary>
    /// Chicken meat.
    /// </summary>
    Chicken = 2,

    /// <summary>
    /// Turkey meat.
    /// </summary>
    Turkey = 3,

    /// <summary>
    /// Lamb meat.
    /// </summary>
    Lamb = 4,

    /// <summary>
    /// Fish or seafood.
    /// </summary>
    Seafood = 5,

    /// <summary>
    /// Vegetables.
    /// </summary>
    Vegetables = 6,

    /// <summary>
    /// Mixed meats.
    /// </summary>
    Mixed = 7,

    /// <summary>
    /// Other type.
    /// </summary>
    Other = 8,
}
