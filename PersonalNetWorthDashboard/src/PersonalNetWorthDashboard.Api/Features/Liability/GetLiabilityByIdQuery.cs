// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Liability;

public record GetLiabilityByIdQuery(Guid LiabilityId) : IRequest<LiabilityDto>;

public class GetLiabilityByIdQueryHandler : IRequestHandler<GetLiabilityByIdQuery, LiabilityDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public GetLiabilityByIdQueryHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<LiabilityDto> Handle(GetLiabilityByIdQuery request, CancellationToken cancellationToken)
    {
        var liability = await _context.Liabilities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LiabilityId == request.LiabilityId, cancellationToken)
            ?? throw new InvalidOperationException($"Liability with ID {request.LiabilityId} not found.");

        return liability.ToDto();
    }
}
