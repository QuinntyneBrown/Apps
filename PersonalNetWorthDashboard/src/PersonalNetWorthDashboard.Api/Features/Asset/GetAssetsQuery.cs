// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Asset;

public record GetAssetsQuery() : IRequest<List<AssetDto>>;

public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, List<AssetDto>>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public GetAssetsQueryHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<List<AssetDto>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Assets
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
