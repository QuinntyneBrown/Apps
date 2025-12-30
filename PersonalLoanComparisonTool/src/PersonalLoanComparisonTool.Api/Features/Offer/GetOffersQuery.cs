// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Offer;

public record GetOffersQuery() : IRequest<List<OfferDto>>;

public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, List<OfferDto>>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public GetOffersQueryHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<List<OfferDto>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Offers
            .Select(o => o.ToDto())
            .ToListAsync(cancellationToken);
    }
}
