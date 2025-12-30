// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get a random prompt with optional filters.
/// </summary>
public record GetRandomPromptQuery : IRequest<PromptDto?>
{
    /// <summary>
    /// Gets or sets the optional category filter.
    /// </summary>
    public int? Category { get; init; }

    /// <summary>
    /// Gets or sets the optional depth filter.
    /// </summary>
    public int? Depth { get; init; }
}

/// <summary>
/// Handler for GetRandomPromptQuery.
/// </summary>
public class GetRandomPromptQueryHandler : IRequestHandler<GetRandomPromptQuery, PromptDto?>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetRandomPromptQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRandomPromptQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetRandomPromptQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetRandomPromptQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PromptDto?> Handle(GetRandomPromptQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting random prompt with filters - Category: {Category}, Depth: {Depth}", request.Category, request.Depth);

        var query = _context.Prompts.AsNoTracking();

        if (request.Category.HasValue)
        {
            query = query.Where(x => (int)x.Category == request.Category.Value);
        }

        if (request.Depth.HasValue)
        {
            query = query.Where(x => (int)x.Depth == request.Depth.Value);
        }

        var prompts = await query.ToListAsync(cancellationToken);

        if (!prompts.Any())
        {
            _logger.LogInformation("No prompts found matching the criteria");
            return null;
        }

        var random = new Random();
        var randomPrompt = prompts[random.Next(prompts.Count)];

        return randomPrompt.ToDto();
    }
}
