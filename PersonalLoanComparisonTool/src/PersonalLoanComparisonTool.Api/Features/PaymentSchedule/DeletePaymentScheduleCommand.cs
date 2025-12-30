// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

public record DeletePaymentScheduleCommand(Guid PaymentScheduleId) : IRequest;

public class DeletePaymentScheduleCommandHandler : IRequestHandler<DeletePaymentScheduleCommand>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public DeletePaymentScheduleCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePaymentScheduleCommand request, CancellationToken cancellationToken)
    {
        var paymentSchedule = await _context.PaymentSchedules
            .FirstOrDefaultAsync(ps => ps.PaymentScheduleId == request.PaymentScheduleId, cancellationToken);

        if (paymentSchedule == null)
        {
            throw new InvalidOperationException($"PaymentSchedule with ID {request.PaymentScheduleId} not found");
        }

        _context.PaymentSchedules.Remove(paymentSchedule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
