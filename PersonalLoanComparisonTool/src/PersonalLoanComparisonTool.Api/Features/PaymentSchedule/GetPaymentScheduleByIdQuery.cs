// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

public record GetPaymentScheduleByIdQuery(Guid PaymentScheduleId) : IRequest<PaymentScheduleDto?>;

public class GetPaymentScheduleByIdQueryHandler : IRequestHandler<GetPaymentScheduleByIdQuery, PaymentScheduleDto?>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public GetPaymentScheduleByIdQueryHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<PaymentScheduleDto?> Handle(GetPaymentScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentSchedule = await _context.PaymentSchedules
            .FirstOrDefaultAsync(ps => ps.PaymentScheduleId == request.PaymentScheduleId, cancellationToken);

        return paymentSchedule?.ToDto();
    }
}
