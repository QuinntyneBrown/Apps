// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Models.OpportunityAggregate.Enums;

/// <summary>
/// Defines the status of an opportunity.
/// </summary>
public enum OpportunityStatus
{
    /// <summary>
    /// Opportunity has been identified.
    /// </summary>
    Identified = 0,

    /// <summary>
    /// Actively pursuing the opportunity.
    /// </summary>
    Pursuing = 1,

    /// <summary>
    /// Opportunity has been won.
    /// </summary>
    Won = 2,

    /// <summary>
    /// Opportunity has been lost.
    /// </summary>
    Lost = 3,
}
