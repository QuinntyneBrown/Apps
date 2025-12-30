// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.DateIdeas;

/// <summary>
/// Query to get all date ideas.
/// </summary>
public class GetAllDateIdeasQuery : IRequest<List<DateIdeaDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional category filter.
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Gets or sets the optional budget range filter.
    /// </summary>
    public BudgetRange? BudgetRange { get; set; }

    /// <summary>
    /// Gets or sets whether to filter by favorites only.
    /// </summary>
    public bool? FavoritesOnly { get; set; }
}

/// <summary>
/// Handler for getting all date ideas.
/// </summary>
public class GetAllDateIdeasHandler : IRequestHandler<GetAllDateIdeasQuery, List<DateIdeaDto>>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllDateIdeasHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAllDateIdeasHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<DateIdeaDto>> Handle(GetAllDateIdeasQuery request, CancellationToken cancellationToken)
    {
        var query = _context.DateIdeas
            .Include(d => d.Ratings)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(d => d.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(d => d.Category == request.Category.Value);
        }

        if (request.BudgetRange.HasValue)
        {
            query = query.Where(d => d.BudgetRange == request.BudgetRange.Value);
        }

        if (request.FavoritesOnly.HasValue && request.FavoritesOnly.Value)
        {
            query = query.Where(d => d.IsFavorite);
        }

        var dateIdeas = await query.ToListAsync(cancellationToken);

        return dateIdeas.Select(dateIdea => new DateIdeaDto
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
        }).ToList();
    }
}
