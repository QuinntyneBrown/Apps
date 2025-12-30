// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Warranties;

public class GetWarrantiesByAppliance
{
    public class Query : IRequest<List<WarrantyDto>>
    {
        public Guid ApplianceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<WarrantyDto>>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<WarrantyDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var warranties = await _context.Warranties
                .Where(w => w.ApplianceId == request.ApplianceId)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync(cancellationToken);

            return warranties.Select(w => new WarrantyDto
            {
                WarrantyId = w.WarrantyId,
                ApplianceId = w.ApplianceId,
                Provider = w.Provider,
                StartDate = w.StartDate,
                EndDate = w.EndDate,
                CoverageDetails = w.CoverageDetails,
                DocumentUrl = w.DocumentUrl,
                CreatedAt = w.CreatedAt
            }).ToList();
        }
    }
}
