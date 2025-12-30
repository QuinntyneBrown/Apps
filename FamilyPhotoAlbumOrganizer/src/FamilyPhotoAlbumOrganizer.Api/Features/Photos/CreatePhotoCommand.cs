// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Photos;

/// <summary>
/// Command to create a new photo.
/// </summary>
public class CreatePhotoCommand : IRequest<PhotoDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid? AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file URL.
    /// </summary>
    public string FileUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the thumbnail URL.
    /// </summary>
    public string? ThumbnailUrl { get; set; }

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
/// Validator for CreatePhotoCommand.
/// </summary>
public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePhotoCommandValidator"/> class.
    /// </summary>
    public CreatePhotoCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FileName).NotEmpty().MaximumLength(500);
        RuleFor(x => x.FileUrl).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Caption).MaximumLength(1000);
        RuleFor(x => x.Location).MaximumLength(500);
    }
}

/// <summary>
/// Handler for CreatePhotoCommand.
/// </summary>
public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, PhotoDto>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<CreatePhotoCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePhotoCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public CreatePhotoCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<CreatePhotoCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the CreatePhotoCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created photo DTO.</returns>
    public async Task<PhotoDto> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = request.UserId,
            AlbumId = request.AlbumId,
            FileName = request.FileName,
            FileUrl = request.FileUrl,
            ThumbnailUrl = request.ThumbnailUrl,
            Caption = request.Caption,
            DateTaken = request.DateTaken,
            Location = request.Location,
            IsFavorite = request.IsFavorite,
            CreatedAt = DateTime.UtcNow
        };

        _context.Photos.Add(photo);
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
