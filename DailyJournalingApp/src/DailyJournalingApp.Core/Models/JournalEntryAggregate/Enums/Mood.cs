// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DailyJournalingApp.Core;

/// <summary>
/// Represents the mood for a journal entry.
/// </summary>
public enum Mood
{
    /// <summary>
    /// Very happy mood.
    /// </summary>
    VeryHappy = 0,

    /// <summary>
    /// Happy mood.
    /// </summary>
    Happy = 1,

    /// <summary>
    /// Neutral mood.
    /// </summary>
    Neutral = 2,

    /// <summary>
    /// Sad mood.
    /// </summary>
    Sad = 3,

    /// <summary>
    /// Very sad mood.
    /// </summary>
    VerySad = 4,

    /// <summary>
    /// Anxious mood.
    /// </summary>
    Anxious = 5,

    /// <summary>
    /// Calm mood.
    /// </summary>
    Calm = 6,

    /// <summary>
    /// Energetic mood.
    /// </summary>
    Energetic = 7,

    /// <summary>
    /// Tired mood.
    /// </summary>
    Tired = 8,
}
