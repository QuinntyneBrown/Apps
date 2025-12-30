// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;

namespace DocumentVaultOrganizer.Api.Features.DocumentCategories;

public class CreateDocumentCategory
{
    public record Command : IRequest<DocumentCategoryDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class Handler : IRequestHandler<Command, DocumentCategoryDto>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<DocumentCategoryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = new DocumentCategory
            {
                DocumentCategoryId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.DocumentCategories.Add(category);
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
