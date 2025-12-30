// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Asset;

public record DeleteAssetCommand(Guid AssetId) : IRequest;

public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public DeleteAssetCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets
            .FirstOrDefaultAsync(x => x.AssetId == request.AssetId, cancellationToken)
            ?? throw new InvalidOperationException($"Asset with ID {request.AssetId} not found.");

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
