// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Albums;

/// <summary>
/// Query to get all albums.
/// </summary>
public class GetAlbumsQuery : IRequest<List<AlbumDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetAlbumsQuery.
/// </summary>
public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, List<AlbumDto>>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAlbumsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAlbumsQueryHandler(IFamilyPhotoAlbumOrganizerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetAlbumsQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of album DTOs.</returns>
    public async Task<List<AlbumDto>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Albums.Include(a => a.Photos).AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        var albums = await query
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return albums.Select(album => new AlbumDto
        {
            AlbumId = album.AlbumId,
            UserId = album.UserId,
            Name = album.Name,
            Description = album.Description,
            CoverPhotoUrl = album.CoverPhotoUrl,
            CreatedDate = album.CreatedDate,
            CreatedAt = album.CreatedAt,
            PhotoCount = album.Photos.Count
        }).ToList();
    }
}
