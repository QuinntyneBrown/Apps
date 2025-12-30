// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Albums;

/// <summary>
/// Query to get an album by ID.
/// </summary>
public class GetAlbumByIdQuery : IRequest<AlbumDto?>
{
    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid AlbumId { get; set; }
}

/// <summary>
/// Validator for GetAlbumByIdQuery.
/// </summary>
public class GetAlbumByIdQueryValidator : AbstractValidator<GetAlbumByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAlbumByIdQueryValidator"/> class.
    /// </summary>
    public GetAlbumByIdQueryValidator()
    {
        RuleFor(x => x.AlbumId).NotEmpty();
    }
}

/// <summary>
/// Handler for GetAlbumByIdQuery.
/// </summary>
public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDto?>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<GetAlbumByIdQuery> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAlbumByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public GetAlbumByIdQueryHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<GetAlbumByIdQuery> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the GetAlbumByIdQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The album DTO or null if not found.</returns>
    public async Task<AlbumDto?> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.AlbumId == request.AlbumId, cancellationToken);

        if (album == null)
        {
            return null;
        }

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
