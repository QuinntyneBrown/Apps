// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.DocumentCategories;

public class GetDocumentCategories
{
    public record Query : IRequest<List<DocumentCategoryDto>>;

    public class Handler : IRequestHandler<Query, List<DocumentCategoryDto>>
    {
        private readonly IDocumentVaultOrganizerContext _context;

        public Handler(IDocumentVaultOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentCategoryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await _context.DocumentCategories
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return categories.Select(c => new DocumentCategoryDto
            {
                DocumentCategoryId = c.DocumentCategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
    }
}
