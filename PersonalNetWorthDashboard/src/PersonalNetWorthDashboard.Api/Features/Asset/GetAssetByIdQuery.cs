// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Asset;

public record GetAssetByIdQuery(Guid AssetId) : IRequest<AssetDto>;

public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, AssetDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public GetAssetByIdQueryHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<AssetDto> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.AssetId == request.AssetId, cancellationToken)
            ?? throw new InvalidOperationException($"Asset with ID {request.AssetId} not found.");

        return asset.ToDto();
    }
}
