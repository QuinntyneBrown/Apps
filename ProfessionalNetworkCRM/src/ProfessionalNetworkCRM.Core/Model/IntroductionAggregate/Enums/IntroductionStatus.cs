// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Model.IntroductionAggregate.Enums;

/// <summary>
/// Defines the status of an introduction.
/// </summary>
public enum IntroductionStatus
{
    /// <summary>
    /// Introduction has been requested.
    /// </summary>
    Requested = 0,

    /// <summary>
    /// Introduction has been made.
    /// </summary>
    Made = 1,

    /// <summary>
    /// Introduction was declined.
    /// </summary>
    Declined = 2,
}
