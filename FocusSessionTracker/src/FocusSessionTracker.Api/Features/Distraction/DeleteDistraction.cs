// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.Distraction;

/// <summary>
/// Command to delete a distraction.
/// </summary>
public class DeleteDistractionCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the distraction ID.
    /// </summary>
    public Guid DistractionId { get; set; }
}

/// <summary>
/// Handler for deleting a distraction.
/// </summary>
public class DeleteDistractionCommandHandler : IRequestHandler<DeleteDistractionCommand, bool>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDistractionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteDistractionCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteDistractionCommand request, CancellationToken cancellationToken)
    {
        var distraction = await _context.Distractions
            .FirstOrDefaultAsync(d => d.DistractionId == request.DistractionId, cancellationToken);

        if (distraction == null)
        {
            return false;
        }

        _context.Distractions.Remove(distraction);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
