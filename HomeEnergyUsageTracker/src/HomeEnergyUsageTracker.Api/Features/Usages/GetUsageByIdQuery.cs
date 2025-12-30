// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.Usages;

public class GetUsageByIdQuery : IRequest<UsageDto>
{
    public Guid UsageId { get; set; }
}

public class GetUsageByIdQueryHandler : IRequestHandler<GetUsageByIdQuery, UsageDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public GetUsageByIdQueryHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<UsageDto> Handle(GetUsageByIdQuery request, CancellationToken cancellationToken)
    {
        var usage = await _context.Usages
            .FirstOrDefaultAsync(x => x.UsageId == request.UsageId, cancellationToken);

        if (usage == null)
        {
            throw new KeyNotFoundException($"Usage with id {request.UsageId} not found.");
        }

        return UsageDto.FromUsage(usage);
    }
}
