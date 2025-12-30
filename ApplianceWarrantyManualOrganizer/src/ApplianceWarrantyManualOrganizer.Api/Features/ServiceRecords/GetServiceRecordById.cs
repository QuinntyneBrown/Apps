// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;

public class GetServiceRecordById
{
    public class Query : IRequest<ServiceRecordDto?>
    {
        public Guid ServiceRecordId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ServiceRecordDto?>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ServiceRecordDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var serviceRecord = await _context.ServiceRecords
                .FirstOrDefaultAsync(sr => sr.ServiceRecordId == request.ServiceRecordId, cancellationToken);

            if (serviceRecord == null)
            {
                return null;
            }

            return new ServiceRecordDto
            {
                ServiceRecordId = serviceRecord.ServiceRecordId,
                ApplianceId = serviceRecord.ApplianceId,
                ServiceDate = serviceRecord.ServiceDate,
                ServiceProvider = serviceRecord.ServiceProvider,
                Description = serviceRecord.Description,
                Cost = serviceRecord.Cost,
                CreatedAt = serviceRecord.CreatedAt
            };
        }
    }
}
