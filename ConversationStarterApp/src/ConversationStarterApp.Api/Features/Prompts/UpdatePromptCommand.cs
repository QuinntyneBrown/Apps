// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to update an existing prompt.
/// </summary>
public record UpdatePromptCommand : IRequest<PromptDto?>
{
    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }

    /// <summary>
    /// Gets or sets the text of the prompt.
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the prompt.
    /// </summary>
    public Category Category { get; init; }

    /// <summary>
    /// Gets or sets the depth level of the prompt.
    /// </summary>
    public Depth Depth { get; init; }

    /// <summary>
    /// Gets or sets tags associated with this prompt.
    /// </summary>
    public string? Tags { get; init; }
}

/// <summary>
/// Handler for UpdatePromptCommand.
/// </summary>
public class UpdatePromptCommandHandler : IRequestHandler<UpdatePromptCommand, PromptDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<UpdatePromptCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePromptCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdatePromptCommandHandler(
        IConversationStarterAppContext context,
        ILogger<UpdatePromptCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PromptDto?> Handle(UpdatePromptCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating prompt {PromptId}", request.PromptId);

        var prompt = await _context.Prompts
            .FirstOrDefaultAsync(x => x.PromptId == request.PromptId, cancellationToken);

        if (prompt == null)
        {
            _logger.LogWarning("Prompt {PromptId} not found", request.PromptId);
            return null;
        }

        prompt.Text = request.Text;
        prompt.Category = request.Category;
        prompt.Depth = request.Depth;
        prompt.Tags = request.Tags;
        prompt.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated prompt {PromptId}", request.PromptId);

        return prompt.ToDto();
    }
}
