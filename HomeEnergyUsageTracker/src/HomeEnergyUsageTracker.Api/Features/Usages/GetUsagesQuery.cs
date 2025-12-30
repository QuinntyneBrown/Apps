// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.Usages;

public class GetUsagesQuery : IRequest<List<UsageDto>>
{
    public Guid? UtilityBillId { get; set; }
}

public class GetUsagesQueryHandler : IRequestHandler<GetUsagesQuery, List<UsageDto>>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public GetUsagesQueryHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<UsageDto>> Handle(GetUsagesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Usages.AsQueryable();

        if (request.UtilityBillId.HasValue)
        {
            query = query.Where(x => x.UtilityBillId == request.UtilityBillId.Value);
        }

        var usages = await query
            .OrderByDescending(x => x.Date)
            .ToListAsync(cancellationToken);

        return usages.Select(UsageDto.FromUsage).ToList();
    }
}
