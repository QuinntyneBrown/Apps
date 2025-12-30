// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Appliances;

public class GetAppliances
{
    public class Query : IRequest<List<ApplianceDto>>
    {
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ApplianceDto>>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<ApplianceDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Appliances.AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == request.UserId.Value);
            }

            var appliances = await query
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync(cancellationToken);

            return appliances.Select(a => new ApplianceDto
            {
                ApplianceId = a.ApplianceId,
                UserId = a.UserId,
                Name = a.Name,
                ApplianceType = a.ApplianceType,
                Brand = a.Brand,
                ModelNumber = a.ModelNumber,
                SerialNumber = a.SerialNumber,
                PurchaseDate = a.PurchaseDate,
                PurchasePrice = a.PurchasePrice,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
    }
}
