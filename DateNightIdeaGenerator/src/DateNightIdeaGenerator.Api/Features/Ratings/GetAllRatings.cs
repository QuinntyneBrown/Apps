// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Ratings;

/// <summary>
/// Query to get all ratings.
/// </summary>
public class GetAllRatingsQuery : IRequest<List<RatingDto>>
{
    /// <summary>
    /// Gets or sets the optional date idea ID filter.
    /// </summary>
    public Guid? DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the optional experience ID filter.
    /// </summary>
    public Guid? ExperienceId { get; set; }

    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for getting all ratings.
/// </summary>
public class GetAllRatingsHandler : IRequestHandler<GetAllRatingsQuery, List<RatingDto>>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllRatingsHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAllRatingsHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<RatingDto>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Ratings.AsQueryable();

        if (request.DateIdeaId.HasValue)
        {
            query = query.Where(r => r.DateIdeaId == request.DateIdeaId.Value);
        }

        if (request.ExperienceId.HasValue)
        {
            query = query.Where(r => r.ExperienceId == request.ExperienceId.Value);
        }

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        var ratings = await query.ToListAsync(cancellationToken);

        return ratings.Select(rating => new RatingDto
        {
            RatingId = rating.RatingId,
            DateIdeaId = rating.DateIdeaId,
            ExperienceId = rating.ExperienceId,
            UserId = rating.UserId,
            Score = rating.Score,
            Review = rating.Review,
            CreatedAt = rating.CreatedAt,
            UpdatedAt = rating.UpdatedAt
        }).ToList();
    }
}
