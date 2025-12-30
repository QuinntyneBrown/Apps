// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;

namespace InvestmentPortfolioTracker.Api.Features.Dividend;

/// <summary>
/// Data transfer object for Dividend.
/// </summary>
public record DividendDto
{
    public Guid DividendId { get; set; }
    public Guid HoldingId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ExDividendDate { get; set; }
    public decimal AmountPerShare { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsReinvested { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Extension methods for Dividend entity.
/// </summary>
public static class DividendExtensions
{
    /// <summary>
    /// Converts a Dividend entity to DividendDto.
    /// </summary>
    public static DividendDto ToDto(this Core.Dividend dividend)
    {
        return new DividendDto
        {
            DividendId = dividend.DividendId,
            HoldingId = dividend.HoldingId,
            PaymentDate = dividend.PaymentDate,
            ExDividendDate = dividend.ExDividendDate,
            AmountPerShare = dividend.AmountPerShare,
            TotalAmount = dividend.TotalAmount,
            IsReinvested = dividend.IsReinvested,
            Notes = dividend.Notes
        };
    }
}
