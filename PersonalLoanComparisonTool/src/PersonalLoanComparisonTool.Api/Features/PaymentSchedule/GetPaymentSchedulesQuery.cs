// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.PaymentSchedule;

public record GetPaymentSchedulesQuery() : IRequest<List<PaymentScheduleDto>>;

public class GetPaymentSchedulesQueryHandler : IRequestHandler<GetPaymentSchedulesQuery, List<PaymentScheduleDto>>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public GetPaymentSchedulesQueryHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentScheduleDto>> Handle(GetPaymentSchedulesQuery request, CancellationToken cancellationToken)
    {
        return await _context.PaymentSchedules
            .Select(ps => ps.ToDto())
            .ToListAsync(cancellationToken);
    }
}
