// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Offer;

public record CreateOfferCommand(
    Guid LoanId,
    string LenderName,
    decimal LoanAmount,
    decimal InterestRate,
    int TermMonths,
    decimal MonthlyPayment,
    decimal Fees,
    string? Notes
) : IRequest<OfferDto>;

public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, OfferDto>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public CreateOfferCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<OfferDto> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = new Core.Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = request.LoanId,
            LenderName = request.LenderName,
            LoanAmount = request.LoanAmount,
            InterestRate = request.InterestRate,
            TermMonths = request.TermMonths,
            MonthlyPayment = request.MonthlyPayment,
            Fees = request.Fees,
            Notes = request.Notes
        };

        offer.CalculateTotalCost();

        _context.Offers.Add(offer);
        await _context.SaveChangesAsync(cancellationToken);

        return offer.ToDto();
    }
}
