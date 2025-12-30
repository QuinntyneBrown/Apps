// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Ratings;

/// <summary>
/// Query to get a rating by ID.
/// </summary>
public class GetRatingByIdQuery : IRequest<RatingDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier for the rating.
    /// </summary>
    public Guid RatingId { get; set; }
}

/// <summary>
/// Handler for getting a rating by ID.
/// </summary>
public class GetRatingByIdHandler : IRequestHandler<GetRatingByIdQuery, RatingDto?>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRatingByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRatingByIdHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RatingDto?> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
    {
        var rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.RatingId == request.RatingId, cancellationToken);

        if (rating == null)
        {
            return null;
        }

        return new RatingDto
        {
            RatingId = rating.RatingId,
            DateIdeaId = rating.DateIdeaId,
            ExperienceId = rating.ExperienceId,
            UserId = rating.UserId,
            Score = rating.Score,
            Review = rating.Review,
            CreatedAt = rating.CreatedAt,
            UpdatedAt = rating.UpdatedAt
        };
    }
}
