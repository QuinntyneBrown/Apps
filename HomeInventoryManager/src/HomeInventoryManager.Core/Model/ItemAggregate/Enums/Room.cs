// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core;

/// <summary>
/// Represents the room location of an item.
/// </summary>
public enum Room
{
    /// <summary>
    /// Living room.
    /// </summary>
    LivingRoom = 0,

    /// <summary>
    /// Bedroom.
    /// </summary>
    Bedroom = 1,

    /// <summary>
    /// Kitchen.
    /// </summary>
    Kitchen = 2,

    /// <summary>
    /// Dining room.
    /// </summary>
    DiningRoom = 3,

    /// <summary>
    /// Bathroom.
    /// </summary>
    Bathroom = 4,

    /// <summary>
    /// Garage.
    /// </summary>
    Garage = 5,

    /// <summary>
    /// Basement.
    /// </summary>
    Basement = 6,

    /// <summary>
    /// Attic.
    /// </summary>
    Attic = 7,

    /// <summary>
    /// Office.
    /// </summary>
    Office = 8,

    /// <summary>
    /// Storage.
    /// </summary>
    Storage = 9,

    /// <summary>
    /// Other location.
    /// </summary>
    Other = 10,
}
