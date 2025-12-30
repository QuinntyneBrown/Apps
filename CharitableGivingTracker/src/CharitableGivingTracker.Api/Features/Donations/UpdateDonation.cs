// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Donations;

public class UpdateDonation
{
    public record Command : IRequest<DonationDto?>
    {
        public Guid DonationId { get; init; }
        public Guid OrganizationId { get; init; }
        public decimal Amount { get; init; }
        public DateTime DonationDate { get; init; }
        public DonationType DonationType { get; init; }
        public string? ReceiptNumber { get; init; }
        public bool IsTaxDeductible { get; init; }
        public string? Notes { get; init; }
    }

    public class Handler : IRequestHandler<Command, DonationDto?>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<DonationDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var donation = await _context.Donations
                .FirstOrDefaultAsync(d => d.DonationId == request.DonationId, cancellationToken);

            if (donation == null)
                return null;

            donation.OrganizationId = request.OrganizationId;
            donation.Amount = request.Amount;
            donation.DonationDate = request.DonationDate;
            donation.DonationType = request.DonationType;
            donation.ReceiptNumber = request.ReceiptNumber;
            donation.IsTaxDeductible = request.IsTaxDeductible;
            donation.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            var updatedDonation = await _context.Donations
                .Include(d => d.Organization)
                .FirstOrDefaultAsync(d => d.DonationId == donation.DonationId, cancellationToken);

            return new DonationDto
            {
                DonationId = updatedDonation!.DonationId,
                OrganizationId = updatedDonation.OrganizationId,
                Amount = updatedDonation.Amount,
                DonationDate = updatedDonation.DonationDate,
                DonationType = updatedDonation.DonationType,
                ReceiptNumber = updatedDonation.ReceiptNumber,
                IsTaxDeductible = updatedDonation.IsTaxDeductible,
                Notes = updatedDonation.Notes,
                OrganizationName = updatedDonation.Organization?.Name
            };
        }
    }
}
