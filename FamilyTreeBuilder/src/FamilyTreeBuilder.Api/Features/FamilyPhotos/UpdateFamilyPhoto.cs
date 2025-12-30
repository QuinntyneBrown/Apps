// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.FamilyPhotos;

public class UpdateFamilyPhoto
{
    public class Command : IRequest<FamilyPhotoDto?>
    {
        public Guid FamilyPhotoId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Caption { get; set; }
        public DateTime? DateTaken { get; set; }
    }

    public class Handler : IRequestHandler<Command, FamilyPhotoDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<FamilyPhotoDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var photo = await _context.FamilyPhotos
                .FirstOrDefaultAsync(p => p.FamilyPhotoId == request.FamilyPhotoId, cancellationToken);

            if (photo == null)
            {
                return null;
            }

            photo.PhotoUrl = request.PhotoUrl;
            photo.Caption = request.Caption;
            photo.DateTaken = request.DateTaken;

            await _context.SaveChangesAsync(cancellationToken);

            return FamilyPhotoDto.FromFamilyPhoto(photo);
        }
    }
}
