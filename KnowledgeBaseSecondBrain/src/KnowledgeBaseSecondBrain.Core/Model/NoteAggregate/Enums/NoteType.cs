// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Represents the type of a note.
/// </summary>
public enum NoteType
{
    /// <summary>
    /// Standard text note.
    /// </summary>
    Text = 0,

    /// <summary>
    /// Concept or idea note.
    /// </summary>
    Concept = 1,

    /// <summary>
    /// Reference or resource note.
    /// </summary>
    Reference = 2,

    /// <summary>
    /// Meeting notes.
    /// </summary>
    Meeting = 3,

    /// <summary>
    /// Project notes.
    /// </summary>
    Project = 4,

    /// <summary>
    /// Literature or book notes.
    /// </summary>
    Literature = 5,

    /// <summary>
    /// Daily notes or journal entry.
    /// </summary>
    Daily = 6,

    /// <summary>
    /// Permanent note (Zettelkasten style).
    /// </summary>
    Permanent = 7,

    /// <summary>
    /// Fleeting note (Zettelkasten style).
    /// </summary>
    Fleeting = 8,

    /// <summary>
    /// Other custom note type.
    /// </summary>
    Other = 9,
}
