// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.UtilityBills;

public class GetUtilityBillByIdQuery : IRequest<UtilityBillDto>
{
    public Guid UtilityBillId { get; set; }
}

public class GetUtilityBillByIdQueryHandler : IRequestHandler<GetUtilityBillByIdQuery, UtilityBillDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public GetUtilityBillByIdQueryHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<UtilityBillDto> Handle(GetUtilityBillByIdQuery request, CancellationToken cancellationToken)
    {
        var utilityBill = await _context.UtilityBills
            .FirstOrDefaultAsync(x => x.UtilityBillId == request.UtilityBillId, cancellationToken);

        if (utilityBill == null)
        {
            throw new KeyNotFoundException($"UtilityBill with id {request.UtilityBillId} not found.");
        }

        return UtilityBillDto.FromUtilityBill(utilityBill);
    }
}
