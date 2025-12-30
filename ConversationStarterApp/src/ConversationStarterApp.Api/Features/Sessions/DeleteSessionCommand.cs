// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to delete a session.
/// </summary>
public record DeleteSessionCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid SessionId { get; init; }
}

/// <summary>
/// Handler for DeleteSessionCommand.
/// </summary>
public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, bool>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<DeleteSessionCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteSessionCommandHandler(
        IConversationStarterAppContext context,
        ILogger<DeleteSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .FirstOrDefaultAsync(x => x.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            _logger.LogWarning("Session {SessionId} not found", request.SessionId);
            return false;
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted session {SessionId}", request.SessionId);

        return true;
    }
}
