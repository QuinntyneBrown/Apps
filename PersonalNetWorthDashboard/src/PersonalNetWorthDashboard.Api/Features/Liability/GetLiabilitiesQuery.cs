// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Liability;

public record GetLiabilitiesQuery() : IRequest<List<LiabilityDto>>;

public class GetLiabilitiesQueryHandler : IRequestHandler<GetLiabilitiesQuery, List<LiabilityDto>>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public GetLiabilitiesQueryHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<List<LiabilityDto>> Handle(GetLiabilitiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Liabilities
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
