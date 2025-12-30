// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.PersonTags;

/// <summary>
/// Query to get all person tags.
/// </summary>
public class GetPersonTagsQuery : IRequest<List<PersonTagDto>>
{
    /// <summary>
    /// Gets or sets the optional photo ID filter.
    /// </summary>
    public Guid? PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the optional person name filter.
    /// </summary>
    public string? PersonName { get; set; }
}

/// <summary>
/// Handler for GetPersonTagsQuery.
/// </summary>
public class GetPersonTagsQueryHandler : IRequestHandler<GetPersonTagsQuery, List<PersonTagDto>>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPersonTagsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetPersonTagsQueryHandler(IFamilyPhotoAlbumOrganizerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetPersonTagsQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of person tag DTOs.</returns>
    public async Task<List<PersonTagDto>> Handle(GetPersonTagsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.PersonTags.AsQueryable();

        if (request.PhotoId.HasValue)
        {
            query = query.Where(pt => pt.PhotoId == request.PhotoId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.PersonName))
        {
            query = query.Where(pt => pt.PersonName.Contains(request.PersonName));
        }

        var personTags = await query
            .OrderByDescending(pt => pt.CreatedAt)
            .ToListAsync(cancellationToken);

        return personTags.Select(pt => new PersonTagDto
        {
            PersonTagId = pt.PersonTagId,
            PhotoId = pt.PhotoId,
            PersonName = pt.PersonName,
            CoordinateX = pt.CoordinateX,
            CoordinateY = pt.CoordinateY,
            CreatedAt = pt.CreatedAt
        }).ToList();
    }
}
