// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to create a new session.
/// </summary>
public record CreateSessionCommand : IRequest<SessionDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

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
}

/// <summary>
/// Handler for CreateSessionCommand.
/// </summary>
public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDto>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<CreateSessionCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSessionCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateSessionCommandHandler(
        IConversationStarterAppContext context,
        ILogger<CreateSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<SessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating session for user {UserId} with title: {Title}", request.UserId, request.Title);

        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            StartTime = DateTime.UtcNow,
            Participants = request.Participants,
            PromptsUsed = request.PromptsUsed,
            Notes = request.Notes,
            WasSuccessful = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created session {SessionId}", session.SessionId);

        return session.ToDto();
    }
}
