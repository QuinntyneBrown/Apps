// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Core;

/// <summary>
/// Represents the type of a digital account.
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Email account.
    /// </summary>
    Email = 0,

    /// <summary>
    /// Social media account.
    /// </summary>
    SocialMedia = 1,

    /// <summary>
    /// Financial or banking account.
    /// </summary>
    Financial = 2,

    /// <summary>
    /// Cloud storage account.
    /// </summary>
    CloudStorage = 3,

    /// <summary>
    /// Subscription service.
    /// </summary>
    Subscription = 4,

    /// <summary>
    /// Shopping or e-commerce account.
    /// </summary>
    Shopping = 5,

    /// <summary>
    /// Gaming account.
    /// </summary>
    Gaming = 6,

    /// <summary>
    /// Professional or work account.
    /// </summary>
    Professional = 7,

    /// <summary>
    /// Cryptocurrency or digital wallet.
    /// </summary>
    Cryptocurrency = 8,

    /// <summary>
    /// Other account type.
    /// </summary>
    Other = 9,
}
