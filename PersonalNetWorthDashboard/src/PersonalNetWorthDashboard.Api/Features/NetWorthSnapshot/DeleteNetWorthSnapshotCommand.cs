// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;

public record DeleteNetWorthSnapshotCommand(Guid NetWorthSnapshotId) : IRequest;

public class DeleteNetWorthSnapshotCommandHandler : IRequestHandler<DeleteNetWorthSnapshotCommand>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public DeleteNetWorthSnapshotCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteNetWorthSnapshotCommand request, CancellationToken cancellationToken)
    {
        var snapshot = await _context.NetWorthSnapshots
            .FirstOrDefaultAsync(x => x.NetWorthSnapshotId == request.NetWorthSnapshotId, cancellationToken)
            ?? throw new InvalidOperationException($"NetWorthSnapshot with ID {request.NetWorthSnapshotId} not found.");

        _context.NetWorthSnapshots.Remove(snapshot);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
