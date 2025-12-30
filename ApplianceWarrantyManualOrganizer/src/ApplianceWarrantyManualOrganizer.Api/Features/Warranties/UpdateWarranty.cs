// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Warranties;

public class UpdateWarranty
{
    public class Command : IRequest<WarrantyDto>
    {
        public Guid WarrantyId { get; set; }
        public string? Provider { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CoverageDetails { get; set; }
        public string? DocumentUrl { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.WarrantyId).NotEmpty();
            RuleFor(x => x.Provider).MaximumLength(200);
            RuleFor(x => x.CoverageDetails).MaximumLength(2000);
            RuleFor(x => x.DocumentUrl).MaximumLength(500);
        }
    }

    public class Handler : IRequestHandler<Command, WarrantyDto>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<WarrantyDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var warranty = await _context.Warranties
                .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

            if (warranty == null)
            {
                throw new KeyNotFoundException($"Warranty with ID {request.WarrantyId} not found.");
            }

            warranty.Provider = request.Provider;
            warranty.StartDate = request.StartDate;
            warranty.EndDate = request.EndDate;
            warranty.CoverageDetails = request.CoverageDetails;
            warranty.DocumentUrl = request.DocumentUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return new WarrantyDto
            {
                WarrantyId = warranty.WarrantyId,
                ApplianceId = warranty.ApplianceId,
                Provider = warranty.Provider,
                StartDate = warranty.StartDate,
                EndDate = warranty.EndDate,
                CoverageDetails = warranty.CoverageDetails,
                DocumentUrl = warranty.DocumentUrl,
                CreatedAt = warranty.CreatedAt
            };
        }
    }
}
