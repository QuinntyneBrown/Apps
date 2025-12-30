// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;

namespace DigitalLegacyPlanner.Api.Features.DigitalAccounts;

/// <summary>
/// Data transfer object for DigitalAccount.
/// </summary>
public class DigitalAccountDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the account.
    /// </summary>
    public Guid DigitalAccountId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this account.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the account type.
    /// </summary>
    public AccountType AccountType { get; set; }

    /// <summary>
    /// Gets or sets the account name or service name.
    /// </summary>
    public string AccountName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username or email.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the encrypted password or password hint.
    /// </summary>
    public string? PasswordHint { get; set; }

    /// <summary>
    /// Gets or sets the URL or website.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the account action (delete, memorialize, transfer).
    /// </summary>
    public string? DesiredAction { get; set; }

    /// <summary>
    /// Gets or sets additional notes or instructions.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
