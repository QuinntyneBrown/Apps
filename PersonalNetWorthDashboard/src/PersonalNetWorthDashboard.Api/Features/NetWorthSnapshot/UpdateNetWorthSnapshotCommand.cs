// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;

public record UpdateNetWorthSnapshotCommand(
    Guid NetWorthSnapshotId,
    DateTime SnapshotDate,
    decimal TotalAssets,
    decimal TotalLiabilities,
    string? Notes) : IRequest<NetWorthSnapshotDto>;

public class UpdateNetWorthSnapshotCommandHandler : IRequestHandler<UpdateNetWorthSnapshotCommand, NetWorthSnapshotDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public UpdateNetWorthSnapshotCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<NetWorthSnapshotDto> Handle(UpdateNetWorthSnapshotCommand request, CancellationToken cancellationToken)
    {
        var snapshot = await _context.NetWorthSnapshots
            .FirstOrDefaultAsync(x => x.NetWorthSnapshotId == request.NetWorthSnapshotId, cancellationToken)
            ?? throw new InvalidOperationException($"NetWorthSnapshot with ID {request.NetWorthSnapshotId} not found.");

        snapshot.SnapshotDate = request.SnapshotDate;
        snapshot.TotalAssets = request.TotalAssets;
        snapshot.TotalLiabilities = request.TotalLiabilities;
        snapshot.Notes = request.Notes;
        snapshot.CalculateNetWorth();

        await _context.SaveChangesAsync(cancellationToken);

        return snapshot.ToDto();
    }
}
