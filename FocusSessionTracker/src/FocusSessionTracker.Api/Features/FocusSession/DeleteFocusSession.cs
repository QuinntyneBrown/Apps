// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Command to delete a focus session.
/// </summary>
public class DeleteFocusSessionCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the focus session ID.
    /// </summary>
    public Guid FocusSessionId { get; set; }
}

/// <summary>
/// Handler for deleting a focus session.
/// </summary>
public class DeleteFocusSessionCommandHandler : IRequestHandler<DeleteFocusSessionCommand, bool>
{
    private readonly IFocusSessionTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFocusSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteFocusSessionCommandHandler(IFocusSessionTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.FocusSessionId == request.FocusSessionId, cancellationToken);

        if (session == null)
        {
            return false;
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
