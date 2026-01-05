// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Represents the reading status of a book.
/// </summary>
public enum ReadingStatus
{
    /// <summary>
    /// Book is on the to-read list.
    /// </summary>
    ToRead = 0,

    /// <summary>
    /// Currently reading the book.
    /// </summary>
    CurrentlyReading = 1,

    /// <summary>
    /// Completed reading the book.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Reading was abandoned.
    /// </summary>
    Abandoned = 3,

    /// <summary>
    /// Reading is on hold.
    /// </summary>
    OnHold = 4,
}
