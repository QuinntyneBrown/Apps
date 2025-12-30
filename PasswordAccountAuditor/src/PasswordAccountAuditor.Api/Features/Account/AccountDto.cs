// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.Account;

/// <summary>
/// Data transfer object for Account.
/// </summary>
public record AccountDto
{
    public Guid AccountId { get; init; }
    public Guid UserId { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string? WebsiteUrl { get; init; }
    public AccountCategory Category { get; init; }
    public SecurityLevel SecurityLevel { get; init; }
    public bool HasTwoFactorAuth { get; init; }
    public DateTime? LastPasswordChange { get; init; }
    public DateTime? LastAccessDate { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for mapping Account to AccountDto.
/// </summary>
public static class AccountExtensions
{
    /// <summary>
    /// Converts an Account entity to an AccountDto.
    /// </summary>
    /// <param name="account">The account entity.</param>
    /// <returns>The account DTO.</returns>
    public static AccountDto ToDto(this Core.Account account)
    {
        return new AccountDto
        {
            AccountId = account.AccountId,
            UserId = account.UserId,
            AccountName = account.AccountName,
            Username = account.Username,
            WebsiteUrl = account.WebsiteUrl,
            Category = account.Category,
            SecurityLevel = account.SecurityLevel,
            HasTwoFactorAuth = account.HasTwoFactorAuth,
            LastPasswordChange = account.LastPasswordChange,
            LastAccessDate = account.LastAccessDate,
            Notes = account.Notes,
            IsActive = account.IsActive,
            CreatedAt = account.CreatedAt
        };
    }
}
