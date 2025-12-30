// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;

namespace FamilyTreeBuilder.Api.Features.FamilyPhotos;

public class CreateFamilyPhoto
{
    public class Command : IRequest<FamilyPhotoDto>
    {
        public Guid PersonId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Caption { get; set; }
        public DateTime? DateTaken { get; set; }
    }

    public class Handler : IRequestHandler<Command, FamilyPhotoDto>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<FamilyPhotoDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var photo = new FamilyPhoto
            {
                FamilyPhotoId = Guid.NewGuid(),
                PersonId = request.PersonId,
                PhotoUrl = request.PhotoUrl,
                Caption = request.Caption,
                DateTaken = request.DateTaken,
                CreatedAt = DateTime.UtcNow
            };

            _context.FamilyPhotos.Add(photo);
            await _context.SaveChangesAsync(cancellationToken);

            return FamilyPhotoDto.FromFamilyPhoto(photo);
        }
    }
}
