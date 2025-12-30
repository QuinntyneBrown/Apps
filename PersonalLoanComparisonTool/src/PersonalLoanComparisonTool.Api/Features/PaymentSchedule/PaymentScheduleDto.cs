// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

public record PaymentScheduleDto(
    Guid PaymentScheduleId,
    Guid OfferId,
    int PaymentNumber,
    DateTime DueDate,
    decimal PaymentAmount,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal RemainingBalance
);

public static class PaymentScheduleExtensions
{
    public static PaymentScheduleDto ToDto(this Core.PaymentSchedule paymentSchedule)
    {
        return new PaymentScheduleDto(
            paymentSchedule.PaymentScheduleId,
            paymentSchedule.OfferId,
            paymentSchedule.PaymentNumber,
            paymentSchedule.DueDate,
            paymentSchedule.PaymentAmount,
            paymentSchedule.PrincipalAmount,
            paymentSchedule.InterestAmount,
            paymentSchedule.RemainingBalance
        );
    }
}
