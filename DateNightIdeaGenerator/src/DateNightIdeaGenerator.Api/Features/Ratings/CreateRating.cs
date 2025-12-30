// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;

namespace DateNightIdeaGenerator.Api.Features.Ratings;

/// <summary>
/// Command to create a new rating.
/// </summary>
public class CreateRatingCommand : IRequest<RatingDto>
{
    /// <summary>
    /// Gets or sets the date idea ID this rating is associated with.
    /// </summary>
    public Guid? DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the experience ID this rating is associated with.
    /// </summary>
    public Guid? ExperienceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this rating.
    /// </summary>
    public Guid UserId { get; set; }

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
/// Handler for creating a new rating.
/// </summary>
public class CreateRatingHandler : IRequestHandler<CreateRatingCommand, RatingDto>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRatingHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateRatingHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RatingDto> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = request.DateIdeaId,
            ExperienceId = request.ExperienceId,
            UserId = request.UserId,
            Score = request.Score,
            Review = request.Review,
            CreatedAt = DateTime.UtcNow
        };

        _context.Ratings.Add(rating);
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
