// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;

public record GetNetWorthSnapshotByIdQuery(Guid NetWorthSnapshotId) : IRequest<NetWorthSnapshotDto>;

public class GetNetWorthSnapshotByIdQueryHandler : IRequestHandler<GetNetWorthSnapshotByIdQuery, NetWorthSnapshotDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public GetNetWorthSnapshotByIdQueryHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<NetWorthSnapshotDto> Handle(GetNetWorthSnapshotByIdQuery request, CancellationToken cancellationToken)
    {
        var snapshot = await _context.NetWorthSnapshots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NetWorthSnapshotId == request.NetWorthSnapshotId, cancellationToken)
            ?? throw new InvalidOperationException($"NetWorthSnapshot with ID {request.NetWorthSnapshotId} not found.");

        return snapshot.ToDto();
    }
}
