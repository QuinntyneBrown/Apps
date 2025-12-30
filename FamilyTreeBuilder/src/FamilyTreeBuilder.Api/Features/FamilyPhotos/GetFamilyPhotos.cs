// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.FamilyPhotos;

public class GetFamilyPhotos
{
    public class Query : IRequest<List<FamilyPhotoDto>>
    {
        public Guid? PersonId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<FamilyPhotoDto>>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<List<FamilyPhotoDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.FamilyPhotos.AsQueryable();

            if (request.PersonId.HasValue)
            {
                query = query.Where(p => p.PersonId == request.PersonId.Value);
            }

            var photos = await query
                .OrderByDescending(p => p.DateTaken ?? p.CreatedAt)
                .ToListAsync(cancellationToken);

            return photos.Select(FamilyPhotoDto.FromFamilyPhoto).ToList();
        }
    }
}
