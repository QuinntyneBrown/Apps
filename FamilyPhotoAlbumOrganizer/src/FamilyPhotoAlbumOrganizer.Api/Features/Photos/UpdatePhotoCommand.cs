// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Photos;

/// <summary>
/// Command to update a photo.
/// </summary>
public class UpdatePhotoCommand : IRequest<PhotoDto>
{
    /// <summary>
    /// Gets or sets the photo ID.
    /// </summary>
    public Guid PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid? AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the caption.
    /// </summary>
    public string? Caption { get; set; }

    /// <summary>
    /// Gets or sets the date the photo was taken.
    /// </summary>
    public DateTime? DateTaken { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the photo is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }
}

/// <summary>
/// Validator for UpdatePhotoCommand.
/// </summary>
public class UpdatePhotoCommandValidator : AbstractValidator<UpdatePhotoCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePhotoCommandValidator"/> class.
    /// </summary>
    public UpdatePhotoCommandValidator()
    {
        RuleFor(x => x.PhotoId).NotEmpty();
        RuleFor(x => x.Caption).MaximumLength(1000);
        RuleFor(x => x.Location).MaximumLength(500);
    }
}

/// <summary>
/// Handler for UpdatePhotoCommand.
/// </summary>
public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, PhotoDto>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<UpdatePhotoCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePhotoCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public UpdatePhotoCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<UpdatePhotoCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the UpdatePhotoCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated photo DTO.</returns>
    public async Task<PhotoDto> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var photo = await _context.Photos
            .FirstOrDefaultAsync(p => p.PhotoId == request.PhotoId, cancellationToken);

        if (photo == null)
        {
            throw new KeyNotFoundException($"Photo with ID {request.PhotoId} not found.");
        }

        photo.AlbumId = request.AlbumId;
        photo.Caption = request.Caption;
        photo.DateTaken = request.DateTaken;
        photo.Location = request.Location;
        photo.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

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
