// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;

public record GetNetWorthSnapshotsQuery() : IRequest<List<NetWorthSnapshotDto>>;

public class GetNetWorthSnapshotsQueryHandler : IRequestHandler<GetNetWorthSnapshotsQuery, List<NetWorthSnapshotDto>>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public GetNetWorthSnapshotsQueryHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<List<NetWorthSnapshotDto>> Handle(GetNetWorthSnapshotsQuery request, CancellationToken cancellationToken)
    {
        return await _context.NetWorthSnapshots
            .AsNoTracking()
            .OrderByDescending(x => x.SnapshotDate)
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
