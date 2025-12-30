// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Photos;

/// <summary>
/// Query to get a photo by ID.
/// </summary>
public class GetPhotoByIdQuery : IRequest<PhotoDto?>
{
    /// <summary>
    /// Gets or sets the photo ID.
    /// </summary>
    public Guid PhotoId { get; set; }
}

/// <summary>
/// Validator for GetPhotoByIdQuery.
/// </summary>
public class GetPhotoByIdQueryValidator : AbstractValidator<GetPhotoByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetPhotoByIdQueryValidator"/> class.
    /// </summary>
    public GetPhotoByIdQueryValidator()
    {
        RuleFor(x => x.PhotoId).NotEmpty();
    }
}

/// <summary>
/// Handler for GetPhotoByIdQuery.
/// </summary>
public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, PhotoDto?>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<GetPhotoByIdQuery> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPhotoByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public GetPhotoByIdQueryHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<GetPhotoByIdQuery> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the GetPhotoByIdQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The photo DTO or null if not found.</returns>
    public async Task<PhotoDto?> Handle(GetPhotoByIdQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var photo = await _context.Photos
            .FirstOrDefaultAsync(p => p.PhotoId == request.PhotoId, cancellationToken);

        if (photo == null)
        {
            return null;
        }

        return new PhotoDto
        {
            PhotoId = photo.PhotoId,
            UserId = photo.UserId,
            AlbumId = photo.AlbumId,
            FileName = photo.FileName,
            FileUrl = photo.FileUrl,
            ThumbnailUrl = photo.ThumbnailUrl,
            Caption = photo.Caption,
            DateTaken = photo.DateTaken,
            Location = photo.Location,
            IsFavorite = photo.IsFavorite,
            CreatedAt = photo.CreatedAt
        };
    }
}
