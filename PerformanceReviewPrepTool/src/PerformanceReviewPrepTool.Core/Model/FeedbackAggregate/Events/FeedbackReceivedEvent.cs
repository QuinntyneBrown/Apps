// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Event raised when feedback is received.
/// </summary>
public record FeedbackReceivedEvent
{
    /// <summary>
    /// Gets the feedback ID.
    /// </summary>
    public Guid FeedbackId { get; init; }

    /// <summary>
    /// Gets the review period ID.
    /// </summary>
    public Guid ReviewPeriodId { get; init; }

    /// <summary>
    /// Gets the source.
    /// </summary>
    public string Source { get; init; } = string.Empty;

    /// <summary>
    /// Gets the received date.
    /// </summary>
    public DateTime ReceivedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
