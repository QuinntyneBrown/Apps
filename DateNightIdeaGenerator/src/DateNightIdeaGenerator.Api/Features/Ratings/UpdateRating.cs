// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Ratings;

/// <summary>
/// Command to update an existing rating.
/// </summary>
public class UpdateRatingCommand : IRequest<RatingDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier for the rating.
    /// </summary>
    public Guid RatingId { get; set; }

    /// <summary>
    /// Gets or sets the rating score (1-5).
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the review text.
    /// </summary>
    public string? Review { get; set; }
}

/// <summary>
/// Handler for updating a rating.
/// </summary>
public class UpdateRatingHandler : IRequestHandler<UpdateRatingCommand, RatingDto?>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRatingHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateRatingHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RatingDto?> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.RatingId == request.RatingId, cancellationToken);

        if (rating == null)
        {
            return null;
        }

        rating.UpdateScore(request.Score);
        rating.Review = request.Review;

        await _context.SaveChangesAsync(cancellationToken);

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
