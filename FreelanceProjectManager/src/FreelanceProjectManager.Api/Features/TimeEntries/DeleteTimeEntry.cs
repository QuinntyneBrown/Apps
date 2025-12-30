// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.TimeEntries;

/// <summary>
/// Command to delete a time entry.
/// </summary>
public class DeleteTimeEntryCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the time entry ID.
    /// </summary>
    public Guid TimeEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for deleting a time entry.
/// </summary>
public class DeleteTimeEntryHandler : IRequestHandler<DeleteTimeEntryCommand, bool>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTimeEntryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteTimeEntryHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteTimeEntryCommand request, CancellationToken cancellationToken)
    {
        var timeEntry = await _context.TimeEntries
            .Where(te => te.TimeEntryId == request.TimeEntryId && te.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (timeEntry == null)
        {
            return false;
        }

        _context.TimeEntries.Remove(timeEntry);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
