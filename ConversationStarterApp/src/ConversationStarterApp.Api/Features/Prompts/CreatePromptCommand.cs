// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to create a new prompt.
/// </summary>
public record CreatePromptCommand : IRequest<PromptDto>
{
    /// <summary>
    /// Gets or sets the user ID (null for system prompts).
    /// </summary>
    public Guid? UserId { get; init; }

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

    /// <summary>
    /// Gets or sets a value indicating whether this is a system-provided prompt.
    /// </summary>
    public bool IsSystemPrompt { get; init; }
}

/// <summary>
/// Handler for CreatePromptCommand.
/// </summary>
public class CreatePromptCommandHandler : IRequestHandler<CreatePromptCommand, PromptDto>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<CreatePromptCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePromptCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreatePromptCommandHandler(
        IConversationStarterAppContext context,
        ILogger<CreatePromptCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PromptDto> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating prompt with text: {Text}", request.Text);

        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            UserId = request.UserId,
            Text = request.Text,
            Category = request.Category,
            Depth = request.Depth,
            Tags = request.Tags,
            IsSystemPrompt = request.IsSystemPrompt,
            UsageCount = 0,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created prompt {PromptId}", prompt.PromptId);

        return prompt.ToDto();
    }
}
