// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Warranties;

public class GetWarrantyById
{
    public class Query : IRequest<WarrantyDto?>
    {
        public Guid WarrantyId { get; set; }
    }

    public class Handler : IRequestHandler<Query, WarrantyDto?>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<WarrantyDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var warranty = await _context.Warranties
                .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

            if (warranty == null)
            {
                return null;
            }

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
