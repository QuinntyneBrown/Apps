// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.DateIdeas;

/// <summary>
/// Command to create a new date idea.
/// </summary>
public class CreateDateIdeaCommand : IRequest<DateIdeaDto>
{
    /// <summary>
    /// Gets or sets the user ID who created this idea.
    /// </summary>
    public Guid UserId { get; set; }

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
/// Handler for creating a new date idea.
/// </summary>
public class CreateDateIdeaHandler : IRequestHandler<CreateDateIdeaCommand, DateIdeaDto>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDateIdeaHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateDateIdeaHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<DateIdeaDto> Handle(CreateDateIdeaCommand request, CancellationToken cancellationToken)
    {
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            BudgetRange = request.BudgetRange,
            Location = request.Location,
            DurationMinutes = request.DurationMinutes,
            Season = request.Season,
            CreatedAt = DateTime.UtcNow
        };

        _context.DateIdeas.Add(dateIdea);
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
