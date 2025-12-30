// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get a favorite by ID.
/// </summary>
public record GetFavoriteByIdQuery : IRequest<FavoriteDto?>
{
    /// <summary>
    /// Gets or sets the favorite ID.
    /// </summary>
    public Guid FavoriteId { get; init; }
}

/// <summary>
/// Handler for GetFavoriteByIdQuery.
/// </summary>
public class GetFavoriteByIdQueryHandler : IRequestHandler<GetFavoriteByIdQuery, FavoriteDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetFavoriteByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFavoriteByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetFavoriteByIdQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetFavoriteByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<FavoriteDto?> Handle(GetFavoriteByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting favorite {FavoriteId}", request.FavoriteId);

        var favorite = await _context.Favorites
            .AsNoTracking()
            .Include(x => x.Prompt)
            .FirstOrDefaultAsync(x => x.FavoriteId == request.FavoriteId, cancellationToken);

        if (favorite == null)
        {
            _logger.LogInformation("Favorite {FavoriteId} not found", request.FavoriteId);
            return null;
        }

        return favorite.ToDto();
    }
}
