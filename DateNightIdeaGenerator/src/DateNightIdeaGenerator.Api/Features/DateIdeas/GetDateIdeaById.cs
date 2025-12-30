// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.DateIdeas;

/// <summary>
/// Query to get a date idea by ID.
/// </summary>
public class GetDateIdeaByIdQuery : IRequest<DateIdeaDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier for the date idea.
    /// </summary>
    public Guid DateIdeaId { get; set; }
}

/// <summary>
/// Handler for getting a date idea by ID.
/// </summary>
public class GetDateIdeaByIdHandler : IRequestHandler<GetDateIdeaByIdQuery, DateIdeaDto?>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDateIdeaByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetDateIdeaByIdHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<DateIdeaDto?> Handle(GetDateIdeaByIdQuery request, CancellationToken cancellationToken)
    {
        var dateIdea = await _context.DateIdeas
            .Include(d => d.Ratings)
            .FirstOrDefaultAsync(d => d.DateIdeaId == request.DateIdeaId, cancellationToken);

        if (dateIdea == null)
        {
            return null;
        }

        return new DateIdeaDto
        {
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            Title = dateIdea.Title,
            Description = dateIdea.Description,
            Category = dateIdea.Category,
            BudgetRange = dateIdea.BudgetRange,
            Location = dateIdea.Location,
            DurationMinutes = dateIdea.DurationMinutes,
            Season = dateIdea.Season,
            IsFavorite = dateIdea.IsFavorite,
            HasBeenTried = dateIdea.HasBeenTried,
            CreatedAt = dateIdea.CreatedAt,
            UpdatedAt = dateIdea.UpdatedAt,
            AverageRating = dateIdea.GetAverageRating()
        };
    }
}
