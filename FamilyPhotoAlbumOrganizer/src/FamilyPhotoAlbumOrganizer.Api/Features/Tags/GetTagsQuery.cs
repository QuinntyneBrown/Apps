// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Tags;

/// <summary>
/// Query to get all tags.
/// </summary>
public class GetTagsQuery : IRequest<List<TagDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetTagsQuery.
/// </summary>
public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<TagDto>>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTagsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTagsQueryHandler(IFamilyPhotoAlbumOrganizerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetTagsQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of tag DTOs.</returns>
    public async Task<List<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tags.Include(t => t.Photos).AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        var tags = await query
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);

        return tags.Select(tag => new TagDto
        {
            TagId = tag.TagId,
            UserId = tag.UserId,
            Name = tag.Name,
            CreatedAt = tag.CreatedAt,
            PhotoCount = tag.Photos.Count
        }).ToList();
    }
}
