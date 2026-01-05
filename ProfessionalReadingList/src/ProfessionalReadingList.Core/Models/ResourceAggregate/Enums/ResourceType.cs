// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core;

/// <summary>
/// Represents the type of a reading resource.
/// </summary>
public enum ResourceType
{
    /// <summary>
    /// A book.
    /// </summary>
    Book = 0,

    /// <summary>
    /// An article.
    /// </summary>
    Article = 1,

    /// <summary>
    /// A research paper.
    /// </summary>
    ResearchPaper = 2,

    /// <summary>
    /// A blog post.
    /// </summary>
    BlogPost = 3,

    /// <summary>
    /// A video or course.
    /// </summary>
    Video = 4,

    /// <summary>
    /// A podcast.
    /// </summary>
    Podcast = 5,

    /// <summary>
    /// A whitepaper.
    /// </summary>
    Whitepaper = 6,

    /// <summary>
    /// Other type of resource.
    /// </summary>
    Other = 7,
}
