// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.UtilityBills;

public class GetUtilityBillsQuery : IRequest<List<UtilityBillDto>>
{
    public Guid? UserId { get; set; }
}

public class GetUtilityBillsQueryHandler : IRequestHandler<GetUtilityBillsQuery, List<UtilityBillDto>>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public GetUtilityBillsQueryHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<UtilityBillDto>> Handle(GetUtilityBillsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.UtilityBills.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        var utilityBills = await query
            .OrderByDescending(x => x.BillingDate)
            .ToListAsync(cancellationToken);

        return utilityBills.Select(UtilityBillDto.FromUtilityBill).ToList();
    }
}
