// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to update an existing favorite.
/// </summary>
public record UpdateFavoriteCommand : IRequest<FavoriteDto?>
{
    /// <summary>
    /// Gets or sets the favorite ID.
    /// </summary>
    public Guid FavoriteId { get; init; }

    /// <summary>
    /// Gets or sets optional notes about why this prompt is favorited.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateFavoriteCommand.
/// </summary>
public class UpdateFavoriteCommandHandler : IRequestHandler<UpdateFavoriteCommand, FavoriteDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<UpdateFavoriteCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFavoriteCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateFavoriteCommandHandler(
        IConversationStarterAppContext context,
        ILogger<UpdateFavoriteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<FavoriteDto?> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating favorite {FavoriteId}", request.FavoriteId);

        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(x => x.FavoriteId == request.FavoriteId, cancellationToken);

        if (favorite == null)
        {
            _logger.LogWarning("Favorite {FavoriteId} not found", request.FavoriteId);
            return null;
        }

        favorite.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated favorite {FavoriteId}", request.FavoriteId);

        return favorite.ToDto();
    }
}
