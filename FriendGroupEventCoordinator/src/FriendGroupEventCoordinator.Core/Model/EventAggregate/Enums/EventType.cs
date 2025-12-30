// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Represents the type of a group event.
/// </summary>
public enum EventType
{
    /// <summary>
    /// Social gathering.
    /// </summary>
    Social = 0,

    /// <summary>
    /// Meal or dining event.
    /// </summary>
    Meal = 1,

    /// <summary>
    /// Sports or fitness activity.
    /// </summary>
    Sports = 2,

    /// <summary>
    /// Outdoor adventure.
    /// </summary>
    Outdoor = 3,

    /// <summary>
    /// Cultural event.
    /// </summary>
    Cultural = 4,

    /// <summary>
    /// Game night.
    /// </summary>
    GameNight = 5,

    /// <summary>
    /// Travel or trip.
    /// </summary>
    Travel = 6,

    /// <summary>
    /// Celebration or party.
    /// </summary>
    Celebration = 7,

    /// <summary>
    /// Meeting or discussion.
    /// </summary>
    Meeting = 8,

    /// <summary>
    /// Other or custom event type.
    /// </summary>
    Other = 9,
}
