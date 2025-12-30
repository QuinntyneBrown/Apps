// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentVaultOrganizer.Api.Features.DocumentCategories;

public class DeleteDocumentCategory
{
    public record Command : IRequest<bool>
    {
        public Guid DocumentCategoryId { get; set; }
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
            var category = await _context.DocumentCategories
                .FirstOrDefaultAsync(c => c.DocumentCategoryId == request.DocumentCategoryId, cancellationToken);

            if (category == null)
            {
                return false;
            }

            _context.DocumentCategories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
