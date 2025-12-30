// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.ExpirationAlerts;

public class AcknowledgeExpirationAlert
{
    public record Command : IRequest<ExpirationAlertDto?>
    {
        public Guid ExpirationAlertId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ExpirationAlertDto?>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ExpirationAlertDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var alert = await _context.ExpirationAlerts
                .FirstOrDefaultAsync(a => a.ExpirationAlertId == request.ExpirationAlertId, cancellationToken);

            if (alert == null)
            {
                return null;
            }

            alert.IsAcknowledged = true;

            await _context.SaveChangesAsync(cancellationToken);

            return new ExpirationAlertDto
            {
                ExpirationAlertId = alert.ExpirationAlertId,
                DocumentId = alert.DocumentId,
                AlertDate = alert.AlertDate,
                IsAcknowledged = alert.IsAcknowledged,
                CreatedAt = alert.CreatedAt
            };
        }
    }
}
