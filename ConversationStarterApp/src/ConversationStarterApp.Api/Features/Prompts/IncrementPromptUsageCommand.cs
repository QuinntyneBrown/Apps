// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Command to increment the usage count of a prompt.
/// </summary>
public record IncrementPromptUsageCommand : IRequest<PromptDto?>
{
    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }
}

/// <summary>
/// Handler for IncrementPromptUsageCommand.
/// </summary>
public class IncrementPromptUsageCommandHandler : IRequestHandler<IncrementPromptUsageCommand, PromptDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<IncrementPromptUsageCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementPromptUsageCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public IncrementPromptUsageCommandHandler(
        IConversationStarterAppContext context,
        ILogger<IncrementPromptUsageCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PromptDto?> Handle(IncrementPromptUsageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Incrementing usage count for prompt {PromptId}", request.PromptId);

        var prompt = await _context.Prompts
            .FirstOrDefaultAsync(x => x.PromptId == request.PromptId, cancellationToken);

        if (prompt == null)
        {
            _logger.LogWarning("Prompt {PromptId} not found", request.PromptId);
            return null;
        }

        prompt.IncrementUsageCount();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Incremented usage count for prompt {PromptId} to {UsageCount}", request.PromptId, prompt.UsageCount);

        return prompt.ToDto();
    }
}
