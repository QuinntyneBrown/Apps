// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Manuals;

public class GetManualsByAppliance
{
    public class Query : IRequest<List<ManualDto>>
    {
        public Guid ApplianceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ManualDto>>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<ManualDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var manuals = await _context.Manuals
                .Where(m => m.ApplianceId == request.ApplianceId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync(cancellationToken);

            return manuals.Select(m => new ManualDto
            {
                ManualId = m.ManualId,
                ApplianceId = m.ApplianceId,
                Title = m.Title,
                FileUrl = m.FileUrl,
                FileType = m.FileType,
                CreatedAt = m.CreatedAt
            }).ToList();
        }
    }
}
