// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Model.IntroductionAggregate.Events;

/// <summary>
/// Event raised when an introduction is requested.
/// </summary>
public record IntroductionRequestedEvent
{
    /// <summary>
    /// Gets the introduction ID.
    /// </summary>
    public Guid IntroductionId { get; init; }

    /// <summary>
    /// Gets the from contact ID.
    /// </summary>
    public Guid FromContactId { get; init; }

    /// <summary>
    /// Gets the to contact ID.
    /// </summary>
    public Guid ToContactId { get; init; }

    /// <summary>
    /// Gets the purpose.
    /// </summary>
    public string Purpose { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
