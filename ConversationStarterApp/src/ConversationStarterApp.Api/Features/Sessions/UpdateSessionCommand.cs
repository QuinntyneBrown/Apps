// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to update an existing session.
/// </summary>
public record UpdateSessionCommand : IRequest<SessionDto?>
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid SessionId { get; init; }

    /// <summary>
    /// Gets or sets the title or name of the session.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the participant names or descriptions.
    /// </summary>
    public string? Participants { get; init; }

    /// <summary>
    /// Gets or sets the prompts used in this session.
    /// </summary>
    public string? PromptsUsed { get; init; }

    /// <summary>
    /// Gets or sets notes or reflections from the session.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the session was successful.
    /// </summary>
    public bool WasSuccessful { get; init; }
}

/// <summary>
/// Handler for UpdateSessionCommand.
/// </summary>
public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, SessionDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<UpdateSessionCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateSessionCommandHandler(
        IConversationStarterAppContext context,
        ILogger<UpdateSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<SessionDto?> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .FirstOrDefaultAsync(x => x.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            _logger.LogWarning("Session {SessionId} not found", request.SessionId);
            return null;
        }

        session.Title = request.Title;
        session.Participants = request.Participants;
        session.PromptsUsed = request.PromptsUsed;
        session.Notes = request.Notes;
        session.WasSuccessful = request.WasSuccessful;
        session.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated session {SessionId}", request.SessionId);

        return session.ToDto();
    }
}
