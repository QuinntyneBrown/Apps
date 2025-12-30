// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Offer;

public record DeleteOfferCommand(Guid OfferId) : IRequest;

public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public DeleteOfferCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers
            .FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);

        if (offer == null)
        {
            throw new InvalidOperationException($"Offer with ID {request.OfferId} not found");
        }

        _context.Offers.Remove(offer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
