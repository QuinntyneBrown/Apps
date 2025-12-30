// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Experiences;

/// <summary>
/// Query to get all experiences.
/// </summary>
public class GetAllExperiencesQuery : IRequest<List<ExperienceDto>>
{
    /// <summary>
    /// Gets or sets the optional date idea ID filter.
    /// </summary>
    public Guid? DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for getting all experiences.
/// </summary>
public class GetAllExperiencesHandler : IRequestHandler<GetAllExperiencesQuery, List<ExperienceDto>>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllExperiencesHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAllExperiencesHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<ExperienceDto>> Handle(GetAllExperiencesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Experiences.AsQueryable();

        if (request.DateIdeaId.HasValue)
        {
            query = query.Where(e => e.DateIdeaId == request.DateIdeaId.Value);
        }

        if (request.UserId.HasValue)
        {
            query = query.Where(e => e.UserId == request.UserId.Value);
        }

        var experiences = await query.ToListAsync(cancellationToken);

        return experiences.Select(experience => new ExperienceDto
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
        }).ToList();
    }
}
