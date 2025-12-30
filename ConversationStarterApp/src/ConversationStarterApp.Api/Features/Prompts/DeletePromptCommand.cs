// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to delete a prompt.
/// </summary>
public record DeletePromptCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }
}

/// <summary>
/// Handler for DeletePromptCommand.
/// </summary>
public class DeletePromptCommandHandler : IRequestHandler<DeletePromptCommand, bool>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<DeletePromptCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePromptCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeletePromptCommandHandler(
        IConversationStarterAppContext context,
        ILogger<DeletePromptCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeletePromptCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting prompt {PromptId}", request.PromptId);

        var prompt = await _context.Prompts
            .FirstOrDefaultAsync(x => x.PromptId == request.PromptId, cancellationToken);

        if (prompt == null)
        {
            _logger.LogWarning("Prompt {PromptId} not found", request.PromptId);
            return false;
        }

        _context.Prompts.Remove(prompt);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted prompt {PromptId}", request.PromptId);

        return true;
    }
}
