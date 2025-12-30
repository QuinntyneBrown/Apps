// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.Documents;

public class UpdateDocument
{
    public record Command : IRequest<DocumentDto?>
    {
        public Guid DocumentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DocumentCategoryEnum Category { get; set; }
        public string? FileUrl { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    public class Handler : IRequestHandler<Command, DocumentDto?>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<DocumentDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.DocumentId == request.DocumentId, cancellationToken);

            if (document == null)
            {
                return null;
            }

            document.Name = request.Name;
            document.Category = request.Category;
            document.FileUrl = request.FileUrl;
            document.ExpirationDate = request.ExpirationDate;

            await _context.SaveChangesAsync(cancellationToken);

            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                UserId = document.UserId,
                Name = document.Name,
                Category = document.Category,
                FileUrl = document.FileUrl,
                ExpirationDate = document.ExpirationDate,
                CreatedAt = document.CreatedAt
            };
        }
    }
}
