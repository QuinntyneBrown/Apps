// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;

public class GetServiceRecordsByAppliance
{
    public class Query : IRequest<List<ServiceRecordDto>>
    {
        public Guid ApplianceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ServiceRecordDto>>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRecordDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var serviceRecords = await _context.ServiceRecords
                .Where(sr => sr.ApplianceId == request.ApplianceId)
                .OrderByDescending(sr => sr.ServiceDate)
                .ToListAsync(cancellationToken);

            return serviceRecords.Select(sr => new ServiceRecordDto
            {
                ServiceRecordId = sr.ServiceRecordId,
                ApplianceId = sr.ApplianceId,
                ServiceDate = sr.ServiceDate,
                ServiceProvider = sr.ServiceProvider,
                Description = sr.Description,
                Cost = sr.Cost,
                CreatedAt = sr.CreatedAt
            }).ToList();
        }
    }
}
