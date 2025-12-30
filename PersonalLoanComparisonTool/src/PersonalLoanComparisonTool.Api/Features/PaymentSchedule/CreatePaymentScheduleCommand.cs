// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

public record CreatePaymentScheduleCommand(
    Guid OfferId,
    int PaymentNumber,
    DateTime DueDate,
    decimal PaymentAmount,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal RemainingBalance
) : IRequest<PaymentScheduleDto>;

public class CreatePaymentScheduleCommandHandler : IRequestHandler<CreatePaymentScheduleCommand, PaymentScheduleDto>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public CreatePaymentScheduleCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<PaymentScheduleDto> Handle(CreatePaymentScheduleCommand request, CancellationToken cancellationToken)
    {
        var paymentSchedule = new Core.PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = request.OfferId,
            PaymentNumber = request.PaymentNumber,
            DueDate = request.DueDate,
            PaymentAmount = request.PaymentAmount,
            PrincipalAmount = request.PrincipalAmount,
            InterestAmount = request.InterestAmount,
            RemainingBalance = request.RemainingBalance
        };

        _context.PaymentSchedules.Add(paymentSchedule);
        await _context.SaveChangesAsync(cancellationToken);

        return paymentSchedule.ToDto();
    }
}
