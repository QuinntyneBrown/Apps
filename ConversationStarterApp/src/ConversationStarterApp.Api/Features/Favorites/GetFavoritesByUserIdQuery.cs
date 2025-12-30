// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get favorites by user ID.
/// </summary>
public record GetFavoritesByUserIdQuery : IRequest<IEnumerable<FavoriteDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetFavoritesByUserIdQuery.
/// </summary>
public class GetFavoritesByUserIdQueryHandler : IRequestHandler<GetFavoritesByUserIdQuery, IEnumerable<FavoriteDto>>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetFavoritesByUserIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFavoritesByUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetFavoritesByUserIdQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetFavoritesByUserIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<FavoriteDto>> Handle(GetFavoritesByUserIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting favorites for user {UserId}", request.UserId);

        var favorites = await _context.Favorites
            .AsNoTracking()
            .Include(x => x.Prompt)
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return favorites.Select(x => x.ToDto());
    }
}
