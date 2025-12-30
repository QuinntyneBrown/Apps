// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;

namespace InvestmentPortfolioTracker.Api.Features.Account;

/// <summary>
/// Data transfer object for Account.
/// </summary>
public record AccountDto
{
    public Guid AccountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string? AccountNumber { get; set; }
    public decimal CurrentBalance { get; set; }
    public bool IsActive { get; set; }
    public DateTime OpenedDate { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Extension methods for Account entity.
/// </summary>
public static class AccountExtensions
{
    /// <summary>
    /// Converts an Account entity to AccountDto.
    /// </summary>
    public static AccountDto ToDto(this Core.Account account)
    {
        return new AccountDto
        {
            AccountId = account.AccountId,
            Name = account.Name,
            AccountType = account.AccountType,
            Institution = account.Institution,
            AccountNumber = account.AccountNumber,
            CurrentBalance = account.CurrentBalance,
            IsActive = account.IsActive,
            OpenedDate = account.OpenedDate,
            Notes = account.Notes
        };
    }
}
