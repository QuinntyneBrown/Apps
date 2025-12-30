// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Photos;

/// <summary>
/// Query to get all photos.
/// </summary>
public class GetPhotosQuery : IRequest<List<PhotoDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional album ID filter.
    /// </summary>
    public Guid? AlbumId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to only get favorites.
    /// </summary>
    public bool? FavoritesOnly { get; set; }
}

/// <summary>
/// Handler for GetPhotosQuery.
/// </summary>
public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQuery, List<PhotoDto>>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPhotosQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetPhotosQueryHandler(IFamilyPhotoAlbumOrganizerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetPhotosQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of photo DTOs.</returns>
    public async Task<List<PhotoDto>> Handle(GetPhotosQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Photos.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (request.AlbumId.HasValue)
        {
            query = query.Where(p => p.AlbumId == request.AlbumId.Value);
        }

        if (request.FavoritesOnly.HasValue && request.FavoritesOnly.Value)
        {
            query = query.Where(p => p.IsFavorite);
        }

        var photos = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return photos.Select(photo => new PhotoDto
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
        }).ToList();
    }
}
