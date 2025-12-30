// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Offer;

public record GetOfferByIdQuery(Guid OfferId) : IRequest<OfferDto?>;

public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, OfferDto?>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public GetOfferByIdQueryHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<OfferDto?> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers
            .FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);

        return offer?.ToDto();
    }
}
