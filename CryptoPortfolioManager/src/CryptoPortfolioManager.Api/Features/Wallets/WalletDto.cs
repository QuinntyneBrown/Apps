// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Api.Features.Wallets;

public class WalletDto
{
    public Guid WalletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string WalletType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
    public decimal TotalValue { get; set; }
}
