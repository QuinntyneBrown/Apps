// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ProfessionalNetworkCRM.Core.Model.OpportunityAggregate.Enums;

namespace ProfessionalNetworkCRM.Core.Model.OpportunityAggregate.Events;

/// <summary>
/// Event raised when a new opportunity is identified.
/// </summary>
public record OpportunityIdentifiedEvent
{
    /// <summary>
    /// Gets the opportunity ID.
    /// </summary>
    public Guid OpportunityId { get; init; }

    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid ContactId { get; init; }

    /// <summary>
    /// Gets the opportunity type.
    /// </summary>
    public OpportunityType Type { get; init; }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
