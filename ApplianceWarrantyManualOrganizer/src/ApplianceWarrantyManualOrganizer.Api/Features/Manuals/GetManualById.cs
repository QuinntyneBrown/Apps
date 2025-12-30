// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Manuals;

public class GetManualById
{
    public class Query : IRequest<ManualDto?>
    {
        public Guid ManualId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ManualDto?>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ManualDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var manual = await _context.Manuals
                .FirstOrDefaultAsync(m => m.ManualId == request.ManualId, cancellationToken);

            if (manual == null)
            {
                return null;
            }

            return new ManualDto
            {
                ManualId = manual.ManualId,
                ApplianceId = manual.ApplianceId,
                Title = manual.Title,
                FileUrl = manual.FileUrl,
                FileType = manual.FileType,
                CreatedAt = manual.CreatedAt
            };
        }
    }
}
