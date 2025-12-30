// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Appliances;

public class GetApplianceById
{
    public class Query : IRequest<ApplianceDto?>
    {
        public Guid ApplianceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ApplianceDto?>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ApplianceDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var appliance = await _context.Appliances
                .FirstOrDefaultAsync(a => a.ApplianceId == request.ApplianceId, cancellationToken);

            if (appliance == null)
            {
                return null;
            }

            return new ApplianceDto
            {
                ApplianceId = appliance.ApplianceId,
                UserId = appliance.UserId,
                Name = appliance.Name,
                ApplianceType = appliance.ApplianceType,
                Brand = appliance.Brand,
                ModelNumber = appliance.ModelNumber,
                SerialNumber = appliance.SerialNumber,
                PurchaseDate = appliance.PurchaseDate,
                PurchasePrice = appliance.PurchasePrice,
                CreatedAt = appliance.CreatedAt
            };
        }
    }
}
