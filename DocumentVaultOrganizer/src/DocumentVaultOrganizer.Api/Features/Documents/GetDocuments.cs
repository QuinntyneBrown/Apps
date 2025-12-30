// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.Documents;

public class GetDocuments
{
    public record Query : IRequest<List<DocumentDto>>
    {
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<DocumentDto>>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Documents.AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query.Where(d => d.UserId == request.UserId.Value);
            }

            var documents = await query
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync(cancellationToken);

            return documents.Select(d => new DocumentDto
            {
                DocumentId = d.DocumentId,
                UserId = d.UserId,
                Name = d.Name,
                Category = d.Category,
                FileUrl = d.FileUrl,
                ExpirationDate = d.ExpirationDate,
                CreatedAt = d.CreatedAt
            }).ToList();
        }
    }
}
