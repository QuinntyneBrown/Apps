// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Represents the type of a contact.
/// </summary>
public enum ContactType
{
    /// <summary>
    /// A professional colleague.
    /// </summary>
    Colleague = 0,

    /// <summary>
    /// A mentor.
    /// </summary>
    Mentor = 1,

    /// <summary>
    /// A mentee.
    /// </summary>
    Mentee = 2,

    /// <summary>
    /// A client.
    /// </summary>
    Client = 3,

    /// <summary>
    /// A recruiter.
    /// </summary>
    Recruiter = 4,

    /// <summary>
    /// An industry peer.
    /// </summary>
    IndustryPeer = 5,

    /// <summary>
    /// A referral source.
    /// </summary>
    Referral = 6,

    /// <summary>
    /// A conference or event connection.
    /// </summary>
    EventConnection = 7,

    /// <summary>
    /// Other type of contact.
    /// </summary>
    Other = 8,
}
