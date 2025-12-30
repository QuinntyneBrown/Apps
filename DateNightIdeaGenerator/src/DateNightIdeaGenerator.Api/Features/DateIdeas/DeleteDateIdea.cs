// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Api.Features.DateIdeas;

/// <summary>
/// Command to delete a date idea.
/// </summary>
public class DeleteDateIdeaCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier for the date idea to delete.
    /// </summary>
    public Guid DateIdeaId { get; set; }
}

/// <summary>
/// Handler for deleting a date idea.
/// </summary>
public class DeleteDateIdeaHandler : IRequestHandler<DeleteDateIdeaCommand, bool>
{
    private readonly IDateNightIdeaGeneratorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDateIdeaHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteDateIdeaHandler(IDateNightIdeaGeneratorContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteDateIdeaCommand request, CancellationToken cancellationToken)
    {
        var dateIdea = await _context.DateIdeas
            .FirstOrDefaultAsync(d => d.DateIdeaId == request.DateIdeaId, cancellationToken);

        if (dateIdea == null)
        {
            return false;
        }

        _context.DateIdeas.Remove(dateIdea);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
