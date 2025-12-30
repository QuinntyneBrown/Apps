// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Represents the depth level of a conversation prompt.
/// </summary>
public enum Depth
{
    /// <summary>
    /// Surface-level, casual questions.
    /// </summary>
    Surface = 0,

    /// <summary>
    /// Moderate depth questions.
    /// </summary>
    Moderate = 1,

    /// <summary>
    /// Deep, meaningful questions.
    /// </summary>
    Deep = 2,

    /// <summary>
    /// Very deep, intimate questions.
    /// </summary>
    Intimate = 3,
}
