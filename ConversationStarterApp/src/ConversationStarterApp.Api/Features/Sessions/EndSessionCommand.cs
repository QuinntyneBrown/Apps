// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to end a session.
/// </summary>
public record EndSessionCommand : IRequest<SessionDto?>
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid SessionId { get; init; }
}

/// <summary>
/// Handler for EndSessionCommand.
/// </summary>
public class EndSessionCommandHandler : IRequestHandler<EndSessionCommand, SessionDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<EndSessionCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public EndSessionCommandHandler(
        IConversationStarterAppContext context,
        ILogger<EndSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<SessionDto?> Handle(EndSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ending session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .FirstOrDefaultAsync(x => x.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            _logger.LogWarning("Session {SessionId} not found", request.SessionId);
            return null;
        }

        session.EndSession();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Ended session {SessionId}", request.SessionId);

        return session.ToDto();
    }
}
