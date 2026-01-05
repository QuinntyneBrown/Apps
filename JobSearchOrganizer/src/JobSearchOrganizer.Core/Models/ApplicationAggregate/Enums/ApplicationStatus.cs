// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Represents the status of a job application.
/// </summary>
public enum ApplicationStatus
{
    /// <summary>
    /// Application is being drafted.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Application has been submitted.
    /// </summary>
    Applied = 1,

    /// <summary>
    /// Application is under review.
    /// </summary>
    UnderReview = 2,

    /// <summary>
    /// Phone screen scheduled or completed.
    /// </summary>
    PhoneScreen = 3,

    /// <summary>
    /// Interview scheduled or in progress.
    /// </summary>
    Interviewing = 4,

    /// <summary>
    /// Offer received.
    /// </summary>
    OfferReceived = 5,

    /// <summary>
    /// Offer accepted.
    /// </summary>
    Accepted = 6,

    /// <summary>
    /// Application rejected.
    /// </summary>
    Rejected = 7,

    /// <summary>
    /// Application withdrawn.
    /// </summary>
    Withdrawn = 8,
}
