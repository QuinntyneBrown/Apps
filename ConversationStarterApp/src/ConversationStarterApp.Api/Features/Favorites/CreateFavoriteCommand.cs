// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to create a new favorite.
/// </summary>
public record CreateFavoriteCommand : IRequest<FavoriteDto>
{
    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets optional notes about why this prompt is favorited.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateFavoriteCommand.
/// </summary>
public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, FavoriteDto>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<CreateFavoriteCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFavoriteCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateFavoriteCommandHandler(
        IConversationStarterAppContext context,
        ILogger<CreateFavoriteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<FavoriteDto> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating favorite for user {UserId}, prompt {PromptId}", request.UserId, request.PromptId);

        var favorite = new Favorite
        {
            FavoriteId = Guid.NewGuid(),
            PromptId = request.PromptId,
            UserId = request.UserId,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created favorite {FavoriteId}", favorite.FavoriteId);

        return favorite.ToDto();
    }
}
