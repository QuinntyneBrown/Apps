// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Experiences;

/// <summary>
/// Command to update an existing experience.
/// </summary>
public class UpdateExperienceCommand : IRequest<ExperienceDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier for the experience.
    /// </summary>
    public Guid ExperienceId { get; set; }

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
    public bool WasSuccessful { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user would repeat this date.
    /// </summary>
    public bool WouldRepeat { get; set; }
}

/// <summary>
/// Handler for updating an experience.
/// </summary>
public class UpdateExperienceHandler : IRequestHandler<UpdateExperienceCommand, ExperienceDto?>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateExperienceHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateExperienceHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ExperienceDto?> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = await _context.Experiences
            .FirstOrDefaultAsync(e => e.ExperienceId == request.ExperienceId, cancellationToken);

        if (experience == null)
        {
            return null;
        }

        experience.ExperienceDate = request.ExperienceDate;
        experience.Notes = request.Notes;
        experience.ActualCost = request.ActualCost;
        experience.Photos = request.Photos;
        experience.WasSuccessful = request.WasSuccessful;
        experience.WouldRepeat = request.WouldRepeat;

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
