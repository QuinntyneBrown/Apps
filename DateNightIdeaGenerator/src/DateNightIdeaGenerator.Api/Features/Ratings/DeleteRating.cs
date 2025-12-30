// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Ratings;

/// <summary>
/// Command to delete a rating.
/// </summary>
public class DeleteRatingCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier for the rating to delete.
    /// </summary>
    public Guid RatingId { get; set; }
}

/// <summary>
/// Handler for deleting a rating.
/// </summary>
public class DeleteRatingHandler : IRequestHandler<DeleteRatingCommand, bool>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRatingHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteRatingHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.RatingId == request.RatingId, cancellationToken);

        if (rating == null)
        {
            return false;
        }

        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
