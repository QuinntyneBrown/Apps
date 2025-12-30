// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.ExpirationAlerts;

public class GetExpirationAlerts
{
    public record Query : IRequest<List<ExpirationAlertDto>>
    {
        public bool? OnlyUnacknowledged { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ExpirationAlertDto>>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<ExpirationAlertDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.ExpirationAlerts.AsQueryable();

            if (request.OnlyUnacknowledged == true)
            {
                query = query.Where(a => !a.IsAcknowledged);
            }

            var alerts = await query
                .OrderBy(a => a.AlertDate)
                .ToListAsync(cancellationToken);

            return alerts.Select(a => new ExpirationAlertDto
            {
                ExpirationAlertId = a.ExpirationAlertId,
                DocumentId = a.DocumentId,
                AlertDate = a.AlertDate,
                IsAcknowledged = a.IsAcknowledged,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
    }
}
