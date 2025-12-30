// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to delete a favorite.
/// </summary>
public record DeleteFavoriteCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the favorite ID.
    /// </summary>
    public Guid FavoriteId { get; init; }
}

/// <summary>
/// Handler for DeleteFavoriteCommand.
/// </summary>
public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, bool>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<DeleteFavoriteCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFavoriteCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteFavoriteCommandHandler(
        IConversationStarterAppContext context,
        ILogger<DeleteFavoriteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting favorite {FavoriteId}", request.FavoriteId);

        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(x => x.FavoriteId == request.FavoriteId, cancellationToken);

        if (favorite == null)
        {
            _logger.LogWarning("Favorite {FavoriteId} not found", request.FavoriteId);
            return false;
        }

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted favorite {FavoriteId}", request.FavoriteId);

        return true;
    }
}
