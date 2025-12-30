// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Albums;

/// <summary>
/// Command to create a new album.
/// </summary>
public class CreateAlbumCommand : IRequest<AlbumDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the album name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the album description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the cover photo URL.
    /// </summary>
    public string? CoverPhotoUrl { get; set; }

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    public DateTime CreatedDate { get; set; }
}

/// <summary>
/// Validator for CreateAlbumCommand.
/// </summary>
public class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAlbumCommandValidator"/> class.
    /// </summary>
    public CreateAlbumCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}

/// <summary>
/// Handler for CreateAlbumCommand.
/// </summary>
public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, AlbumDto>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<CreateAlbumCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAlbumCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public CreateAlbumCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<CreateAlbumCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the CreateAlbumCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created album DTO.</returns>
    public async Task<AlbumDto> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            CoverPhotoUrl = request.CoverPhotoUrl,
            CreatedDate = request.CreatedDate,
            CreatedAt = DateTime.UtcNow
        };

        _context.Albums.Add(album);
        await _context.SaveChangesAsync(cancellationToken);

        return new AlbumDto
        {
            AlbumId = album.AlbumId,
            UserId = album.UserId,
            Name = album.Name,
            Description = album.Description,
            CoverPhotoUrl = album.CoverPhotoUrl,
            CreatedDate = album.CreatedDate,
            CreatedAt = album.CreatedAt,
            PhotoCount = 0
        };
    }
}
