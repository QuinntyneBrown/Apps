// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the category of an account.
/// </summary>
public enum AccountCategory
{
    /// <summary>
    /// Social media accounts.
    /// </summary>
    SocialMedia = 0,

    /// <summary>
    /// Email accounts.
    /// </summary>
    Email = 1,

    /// <summary>
    /// Banking and financial accounts.
    /// </summary>
    Banking = 2,

    /// <summary>
    /// Shopping and e-commerce accounts.
    /// </summary>
    Shopping = 3,

    /// <summary>
    /// Work or professional accounts.
    /// </summary>
    Work = 4,

    /// <summary>
    /// Entertainment and streaming accounts.
    /// </summary>
    Entertainment = 5,

    /// <summary>
    /// Healthcare and medical accounts.
    /// </summary>
    Healthcare = 6,

    /// <summary>
    /// Other or miscellaneous accounts.
    /// </summary>
    Other = 7,
}
