// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Represents the type of an event.
/// </summary>
public enum EventType
{
    /// <summary>
    /// A conference.
    /// </summary>
    Conference = 0,

    /// <summary>
    /// A workshop or training.
    /// </summary>
    Workshop = 1,

    /// <summary>
    /// A seminar.
    /// </summary>
    Seminar = 2,

    /// <summary>
    /// A webinar or virtual event.
    /// </summary>
    Webinar = 3,

    /// <summary>
    /// A networking event.
    /// </summary>
    Networking = 4,

    /// <summary>
    /// A meetup.
    /// </summary>
    Meetup = 5,

    /// <summary>
    /// A trade show or expo.
    /// </summary>
    TradeShow = 6,

    /// <summary>
    /// Other type of event.
    /// </summary>
    Other = 7,
}
