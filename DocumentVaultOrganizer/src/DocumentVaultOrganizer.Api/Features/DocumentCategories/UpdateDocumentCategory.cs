// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.DocumentCategories;

public class UpdateDocumentCategory
{
    public record Command : IRequest<DocumentCategoryDto?>
    {
        public Guid DocumentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class Handler : IRequestHandler<Command, DocumentCategoryDto?>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<DocumentCategoryDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.DocumentCategoryId == request.DocumentCategoryId, cancellationToken);

            if (category == null)
            {
                return null;
            }

            category.Name = request.Name;
            category.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

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
