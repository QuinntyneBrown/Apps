// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.DocumentCategories;

public class GetDocumentCategoryById
{
    public record Query : IRequest<DocumentCategoryDto?>
    {
        public Guid DocumentCategoryId { get; set; }
    }

    public class Handler : IRequestHandler<Query, DocumentCategoryDto?>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<DocumentCategoryDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.DocumentCategoryId == request.DocumentCategoryId, cancellationToken);

            if (category == null)
            {
                return null;
            }

            return new DocumentCategoryDto
            {
                DocumentCategoryId = category.DocumentCategoryId,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt
            };
        }
    }
}
