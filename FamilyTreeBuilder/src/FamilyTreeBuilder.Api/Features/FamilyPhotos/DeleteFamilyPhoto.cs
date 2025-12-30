// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.FamilyPhotos;

public class DeleteFamilyPhoto
{
    public class Command : IRequest<bool>
    {
        public Guid FamilyPhotoId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var photo = await _context.FamilyPhotos
                .FirstOrDefaultAsync(p => p.FamilyPhotoId == request.FamilyPhotoId, cancellationToken);

            if (photo == null)
            {
                return false;
            }

            _context.FamilyPhotos.Remove(photo);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
