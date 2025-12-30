// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get a prompt by ID.
/// </summary>
public record GetPromptByIdQuery : IRequest<PromptDto?>
{
    /// <summary>
    /// Gets or sets the prompt ID.
    /// </summary>
    public Guid PromptId { get; init; }
}

/// <summary>
/// Handler for GetPromptByIdQuery.
/// </summary>
public class GetPromptByIdQueryHandler : IRequestHandler<GetPromptByIdQuery, PromptDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetPromptByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPromptByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetPromptByIdQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetPromptByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PromptDto?> Handle(GetPromptByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting prompt {PromptId}", request.PromptId);

        var prompt = await _context.Prompts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PromptId == request.PromptId, cancellationToken);

        if (prompt == null)
        {
            _logger.LogInformation("Prompt {PromptId} not found", request.PromptId);
            return null;
        }

        return prompt.ToDto();
    }
}
