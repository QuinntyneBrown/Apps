// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get recent sessions for a user.
/// </summary>
public record GetRecentSessionsQuery : IRequest<IEnumerable<SessionDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the number of recent sessions to retrieve.
    /// </summary>
    public int Count { get; init; } = 10;
}

/// <summary>
/// Handler for GetRecentSessionsQuery.
/// </summary>
public class GetRecentSessionsQueryHandler : IRequestHandler<GetRecentSessionsQuery, IEnumerable<SessionDto>>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetRecentSessionsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRecentSessionsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetRecentSessionsQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetRecentSessionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SessionDto>> Handle(GetRecentSessionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting {Count} recent sessions for user {UserId}", request.Count, request.UserId);

        var sessions = await _context.Sessions
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.StartTime)
            .Take(request.Count)
            .ToListAsync(cancellationToken);

        return sessions.Select(x => x.ToDto());
    }
}
