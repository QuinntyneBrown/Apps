// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Donations;

public class CreateDonation
{
    public record Command : IRequest<DonationDto>
    {
        public Guid OrganizationId { get; init; }
        public decimal Amount { get; init; }
        public DateTime DonationDate { get; init; }
        public DonationType DonationType { get; init; }
        public string? ReceiptNumber { get; init; }
        public bool IsTaxDeductible { get; init; } = true;
        public string? Notes { get; init; }
    }

    public class Handler : IRequestHandler<Command, DonationDto>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<DonationDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var donation = new Donation
            {
                DonationId = Guid.NewGuid(),
                OrganizationId = request.OrganizationId,
                Amount = request.Amount,
                DonationDate = request.DonationDate,
                DonationType = request.DonationType,
                ReceiptNumber = request.ReceiptNumber,
                IsTaxDeductible = request.IsTaxDeductible,
                Notes = request.Notes
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync(cancellationToken);

            var createdDonation = await _context.Donations
                .Include(d => d.Organization)
                .FirstOrDefaultAsync(d => d.DonationId == donation.DonationId, cancellationToken);

            return new DonationDto
            {
                DonationId = createdDonation!.DonationId,
                OrganizationId = createdDonation.OrganizationId,
                Amount = createdDonation.Amount,
                DonationDate = createdDonation.DonationDate,
                DonationType = createdDonation.DonationType,
                ReceiptNumber = createdDonation.ReceiptNumber,
                IsTaxDeductible = createdDonation.IsTaxDeductible,
                Notes = createdDonation.Notes,
                OrganizationName = createdDonation.Organization?.Name
            };
        }
    }
}
