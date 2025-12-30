// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.CookSessions;

/// <summary>
/// Command to delete a cook session.
/// </summary>
public class DeleteCookSessionCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the cook session ID.
    /// </summary>
    public Guid CookSessionId { get; set; }
}

/// <summary>
/// Handler for DeleteCookSessionCommand.
/// </summary>
public class DeleteCookSessionCommandHandler : IRequestHandler<DeleteCookSessionCommand, bool>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCookSessionCommandHandler"/> class.
    /// </summary>
    public DeleteCookSessionCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteCookSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.CookSessions
            .FirstOrDefaultAsync(s => s.CookSessionId == request.CookSessionId, cancellationToken);

        if (session == null)
        {
            return false;
        }

        _context.CookSessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
