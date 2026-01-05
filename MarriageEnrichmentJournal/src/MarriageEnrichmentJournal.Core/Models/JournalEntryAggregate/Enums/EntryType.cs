// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MarriageEnrichmentJournal.Core;

/// <summary>
/// Represents the type of journal entry.
/// </summary>
public enum EntryType
{
    /// <summary>
    /// General journal entry.
    /// </summary>
    General = 0,

    /// <summary>
    /// Gratitude entry.
    /// </summary>
    Gratitude = 1,

    /// <summary>
    /// Reflection entry.
    /// </summary>
    Reflection = 2,

    /// <summary>
    /// Prayer or meditation entry.
    /// </summary>
    Prayer = 3,

    /// <summary>
    /// Goal or intention entry.
    /// </summary>
    Goal = 4,

    /// <summary>
    /// Challenge or struggle entry.
    /// </summary>
    Challenge = 5,

    /// <summary>
    /// Celebration or milestone entry.
    /// </summary>
    Celebration = 6,
}
