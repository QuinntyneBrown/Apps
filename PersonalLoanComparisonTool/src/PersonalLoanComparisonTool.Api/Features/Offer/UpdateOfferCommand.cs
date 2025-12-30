// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Offer;

public record UpdateOfferCommand(
    Guid OfferId,
    Guid LoanId,
    string LenderName,
    decimal LoanAmount,
    decimal InterestRate,
    int TermMonths,
    decimal MonthlyPayment,
    decimal Fees,
    string? Notes
) : IRequest<OfferDto>;

public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, OfferDto>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public UpdateOfferCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<OfferDto> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers
            .FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);

        if (offer == null)
        {
            throw new InvalidOperationException($"Offer with ID {request.OfferId} not found");
        }

        offer.LoanId = request.LoanId;
        offer.LenderName = request.LenderName;
        offer.LoanAmount = request.LoanAmount;
        offer.InterestRate = request.InterestRate;
        offer.TermMonths = request.TermMonths;
        offer.MonthlyPayment = request.MonthlyPayment;
        offer.Fees = request.Fees;
        offer.Notes = request.Notes;

        offer.CalculateTotalCost();

        await _context.SaveChangesAsync(cancellationToken);

        return offer.ToDto();
    }
}
