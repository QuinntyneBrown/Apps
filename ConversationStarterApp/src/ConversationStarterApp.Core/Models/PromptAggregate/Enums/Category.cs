// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Represents the category of a conversation prompt.
/// </summary>
public enum Category
{
    /// <summary>
    /// Icebreaker questions.
    /// </summary>
    Icebreaker = 0,

    /// <summary>
    /// Deep conversation questions.
    /// </summary>
    Deep = 1,

    /// <summary>
    /// Fun and lighthearted questions.
    /// </summary>
    Fun = 2,

    /// <summary>
    /// Relationship-building questions.
    /// </summary>
    Relationship = 3,

    /// <summary>
    /// Reflective questions.
    /// </summary>
    Reflective = 4,

    /// <summary>
    /// Hypothetical scenarios.
    /// </summary>
    Hypothetical = 5,

    /// <summary>
    /// Values and beliefs questions.
    /// </summary>
    ValuesAndBeliefs = 6,

    /// <summary>
    /// Dreams and aspirations questions.
    /// </summary>
    DreamsAndAspirations = 7,

    /// <summary>
    /// Past experiences questions.
    /// </summary>
    PastExperiences = 8,

    /// <summary>
    /// Custom or other category.
    /// </summary>
    Other = 9,
}
