// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Albums;

/// <summary>
/// Command to update an album.
/// </summary>
public class UpdateAlbumCommand : IRequest<AlbumDto>
{
    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid AlbumId { get; set; }

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
/// Validator for UpdateAlbumCommand.
/// </summary>
public class UpdateAlbumCommandValidator : AbstractValidator<UpdateAlbumCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAlbumCommandValidator"/> class.
    /// </summary>
    public UpdateAlbumCommandValidator()
    {
        RuleFor(x => x.AlbumId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}

/// <summary>
/// Handler for UpdateAlbumCommand.
/// </summary>
public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, AlbumDto>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<UpdateAlbumCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAlbumCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public UpdateAlbumCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<UpdateAlbumCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the UpdateAlbumCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated album DTO.</returns>
    public async Task<AlbumDto> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.AlbumId == request.AlbumId, cancellationToken);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with ID {request.AlbumId} not found.");
        }

        album.Name = request.Name;
        album.Description = request.Description;
        album.CoverPhotoUrl = request.CoverPhotoUrl;
        album.CreatedDate = request.CreatedDate;

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
            PhotoCount = album.Photos.Count
        };
    }
}
