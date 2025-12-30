// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.Experiences;

/// <summary>
/// Command to delete an experience.
/// </summary>
public class DeleteExperienceCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier for the experience to delete.
    /// </summary>
    public Guid ExperienceId { get; set; }
}

/// <summary>
/// Handler for deleting an experience.
/// </summary>
public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand, bool>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteExperienceHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteExperienceHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = await _context.Experiences
            .FirstOrDefaultAsync(e => e.ExperienceId == request.ExperienceId, cancellationToken);

        if (experience == null)
        {
            return false;
        }

        _context.Experiences.Remove(experience);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
