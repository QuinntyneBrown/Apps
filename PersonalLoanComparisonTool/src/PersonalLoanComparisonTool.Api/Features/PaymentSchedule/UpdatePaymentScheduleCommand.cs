// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

public record UpdatePaymentScheduleCommand(
    Guid PaymentScheduleId,
    Guid OfferId,
    int PaymentNumber,
    DateTime DueDate,
    decimal PaymentAmount,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal RemainingBalance
) : IRequest<PaymentScheduleDto>;

public class UpdatePaymentScheduleCommandHandler : IRequestHandler<UpdatePaymentScheduleCommand, PaymentScheduleDto>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public UpdatePaymentScheduleCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<PaymentScheduleDto> Handle(UpdatePaymentScheduleCommand request, CancellationToken cancellationToken)
    {
        var paymentSchedule = await _context.PaymentSchedules
            .FirstOrDefaultAsync(ps => ps.PaymentScheduleId == request.PaymentScheduleId, cancellationToken);

        if (paymentSchedule == null)
        {
            throw new InvalidOperationException($"PaymentSchedule with ID {request.PaymentScheduleId} not found");
        }

        paymentSchedule.OfferId = request.OfferId;
        paymentSchedule.PaymentNumber = request.PaymentNumber;
        paymentSchedule.DueDate = request.DueDate;
        paymentSchedule.PaymentAmount = request.PaymentAmount;
        paymentSchedule.PrincipalAmount = request.PrincipalAmount;
        paymentSchedule.InterestAmount = request.InterestAmount;
        paymentSchedule.RemainingBalance = request.RemainingBalance;

        await _context.SaveChangesAsync(cancellationToken);

        return paymentSchedule.ToDto();
    }
}
