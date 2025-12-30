// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.ExpirationAlerts;

public class DeleteExpirationAlert
{
    public record Command : IRequest<bool>
    {
        public Guid ExpirationAlertId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var alert = await _context.ExpirationAlerts
                .FirstOrDefaultAsync(a => a.ExpirationAlertId == request.ExpirationAlertId, cancellationToken);

            if (alert == null)
            {
                return false;
            }

            _context.ExpirationAlerts.Remove(alert);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
