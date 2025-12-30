// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;

namespace DateNightIdeaGenerator.Api.Features.Experiences;

/// <summary>
/// Command to create a new experience.
/// </summary>
public class CreateExperienceCommand : IRequest<ExperienceDto>
{
    /// <summary>
    /// Gets or sets the date idea ID this experience is associated with.
    /// </summary>
    public Guid DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who recorded this experience.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the experience occurred.
    /// </summary>
    public DateTime ExperienceDate { get; set; }

    /// <summary>
    /// Gets or sets the notes or journal entry about the experience.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the actual cost of the date.
    /// </summary>
    public decimal? ActualCost { get; set; }

    /// <summary>
    /// Gets or sets the photos or media URLs from the experience.
    /// </summary>
    public string? Photos { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this was a successful date.
    /// </summary>
    public bool WasSuccessful { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the user would repeat this date.
    /// </summary>
    public bool WouldRepeat { get; set; } = true;
}

/// <summary>
/// Handler for creating a new experience.
/// </summary>
public class CreateExperienceHandler : IRequestHandler<CreateExperienceCommand, ExperienceDto>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateExperienceHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateExperienceHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ExperienceDto> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = request.DateIdeaId,
            UserId = request.UserId,
            ExperienceDate = request.ExperienceDate,
            Notes = request.Notes,
            ActualCost = request.ActualCost,
            Photos = request.Photos,
            WasSuccessful = request.WasSuccessful,
            WouldRepeat = request.WouldRepeat,
            CreatedAt = DateTime.UtcNow
        };

        _context.Experiences.Add(experience);
        await _context.SaveChangesAsync(cancellationToken);

        return new ExperienceDto
        {
            ExperienceId = experience.ExperienceId,
            DateIdeaId = experience.DateIdeaId,
            UserId = experience.UserId,
            ExperienceDate = experience.ExperienceDate,
            Notes = experience.Notes,
            ActualCost = experience.ActualCost,
            Photos = experience.Photos,
            WasSuccessful = experience.WasSuccessful,
            WouldRepeat = experience.WouldRepeat,
            CreatedAt = experience.CreatedAt
        };
    }
}
