// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Experiences;

/// <summary>
/// Query to get an experience by ID.
/// </summary>
public class GetExperienceByIdQuery : IRequest<ExperienceDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier for the experience.
    /// </summary>
    public Guid ExperienceId { get; set; }
}

/// <summary>
/// Handler for getting an experience by ID.
/// </summary>
public class GetExperienceByIdHandler : IRequestHandler<GetExperienceByIdQuery, ExperienceDto?>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExperienceByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetExperienceByIdHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ExperienceDto?> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var experience = await _context.Experiences
            .FirstOrDefaultAsync(e => e.ExperienceId == request.ExperienceId, cancellationToken);

        if (experience == null)
        {
            return null;
        }

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
