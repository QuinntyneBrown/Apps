// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;

namespace DocumentVaultOrganizer.Api.Features.ExpirationAlerts;

public class CreateExpirationAlert
{
    public record Command : IRequest<ExpirationAlertDto>
    {
        public Guid DocumentId { get; set; }
        public DateTime AlertDate { get; set; }
    }

    public class Handler : IRequestHandler<Command, ExpirationAlertDto>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ExpirationAlertDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var alert = new ExpirationAlert
            {
                ExpirationAlertId = Guid.NewGuid(),
                DocumentId = request.DocumentId,
                AlertDate = request.AlertDate,
                IsAcknowledged = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.ExpirationAlerts.Add(alert);
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
