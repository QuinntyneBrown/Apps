// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get sessions by user ID.
/// </summary>
public record GetSessionsByUserIdQuery : IRequest<IEnumerable<SessionDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetSessionsByUserIdQuery.
/// </summary>
public class GetSessionsByUserIdQueryHandler : IRequestHandler<GetSessionsByUserIdQuery, IEnumerable<SessionDto>>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetSessionsByUserIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSessionsByUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetSessionsByUserIdQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetSessionsByUserIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SessionDto>> Handle(GetSessionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting sessions for user {UserId}", request.UserId);

        var sessions = await _context.Sessions
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.StartTime)
            .ToListAsync(cancellationToken);

        return sessions.Select(x => x.ToDto());
    }
}
