// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;

public record CreateNetWorthSnapshotCommand(
    DateTime SnapshotDate,
    decimal TotalAssets,
    decimal TotalLiabilities,
    string? Notes) : IRequest<NetWorthSnapshotDto>;

public class CreateNetWorthSnapshotCommandHandler : IRequestHandler<CreateNetWorthSnapshotCommand, NetWorthSnapshotDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public CreateNetWorthSnapshotCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<NetWorthSnapshotDto> Handle(CreateNetWorthSnapshotCommand request, CancellationToken cancellationToken)
    {
        var snapshot = new Core.NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = request.SnapshotDate,
            TotalAssets = request.TotalAssets,
            TotalLiabilities = request.TotalLiabilities,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        snapshot.CalculateNetWorth();

        _context.NetWorthSnapshots.Add(snapshot);
        await _context.SaveChangesAsync(cancellationToken);

        return snapshot.ToDto();
    }
}
