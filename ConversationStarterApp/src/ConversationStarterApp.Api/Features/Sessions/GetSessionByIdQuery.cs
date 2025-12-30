// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get a session by ID.
/// </summary>
public record GetSessionByIdQuery : IRequest<SessionDto?>
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid SessionId { get; init; }
}

/// <summary>
/// Handler for GetSessionByIdQuery.
/// </summary>
public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, SessionDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetSessionByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSessionByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetSessionByIdQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetSessionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<SessionDto?> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            _logger.LogInformation("Session {SessionId} not found", request.SessionId);
            return null;
        }

        return session.ToDto();
    }
}
