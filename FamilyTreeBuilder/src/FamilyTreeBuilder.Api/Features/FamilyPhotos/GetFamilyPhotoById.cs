// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.FamilyPhotos;

public class GetFamilyPhotoById
{
    public class Query : IRequest<FamilyPhotoDto?>
    {
        public Guid FamilyPhotoId { get; set; }
    }

    public class Handler : IRequestHandler<Query, FamilyPhotoDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<FamilyPhotoDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var photo = await _context.FamilyPhotos
                .FirstOrDefaultAsync(p => p.FamilyPhotoId == request.FamilyPhotoId, cancellationToken);

            return photo == null ? null : FamilyPhotoDto.FromFamilyPhoto(photo);
        }
    }
}
