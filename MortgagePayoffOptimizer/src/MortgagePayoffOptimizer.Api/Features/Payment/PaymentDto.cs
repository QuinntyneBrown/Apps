// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Features.Payment;

/// <summary>
/// Data transfer object for Payment entity.
/// </summary>
public record PaymentDto
{
    public Guid PaymentId { get; set; }
    public Guid MortgageId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal? ExtraPrincipal { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Extension methods for mapping Payment to PaymentDto.
/// </summary>
public static class PaymentDtoExtensions
{
    public static PaymentDto ToDto(this Core.Payment payment)
    {
        return new PaymentDto
        {
            PaymentId = payment.PaymentId,
            MortgageId = payment.MortgageId,
            PaymentDate = payment.PaymentDate,
            Amount = payment.Amount,
            PrincipalAmount = payment.PrincipalAmount,
            InterestAmount = payment.InterestAmount,
            ExtraPrincipal = payment.ExtraPrincipal,
            Notes = payment.Notes
        };
    }
}
