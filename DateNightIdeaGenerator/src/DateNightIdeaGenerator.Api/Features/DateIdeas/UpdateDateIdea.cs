// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.DateIdeas;

/// <summary>
/// Command to update an existing date idea.
/// </summary>
public class UpdateDateIdeaCommand : IRequest<DateIdeaDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier for the date idea.
    /// </summary>
    public Guid DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the title of the date idea.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the date idea.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the date idea.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the budget range for this date idea.
    /// </summary>
    public BudgetRange BudgetRange { get; set; }

    /// <summary>
    /// Gets or sets the location for the date.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration in minutes.
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the season suitability.
    /// </summary>
    public string? Season { get; set; }
}

/// <summary>
/// Handler for updating a date idea.
/// </summary>
public class UpdateDateIdeaHandler : IRequestHandler<UpdateDateIdeaCommand, DateIdeaDto?>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDateIdeaHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateDateIdeaHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<DateIdeaDto?> Handle(UpdateDateIdeaCommand request, CancellationToken cancellationToken)
    {
        var dateIdea = await _context.DateIdeas
            .Include(d => d.Ratings)
            .FirstOrDefaultAsync(d => d.DateIdeaId == request.DateIdeaId, cancellationToken);

        if (dateIdea == null)
        {
            return null;
        }

        dateIdea.Title = request.Title;
        dateIdea.Description = request.Description;
        dateIdea.Category = request.Category;
        dateIdea.BudgetRange = request.BudgetRange;
        dateIdea.Location = request.Location;
        dateIdea.DurationMinutes = request.DurationMinutes;
        dateIdea.Season = request.Season;
        dateIdea.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

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
