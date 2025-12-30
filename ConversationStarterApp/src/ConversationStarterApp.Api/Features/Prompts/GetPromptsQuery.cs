// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConversationStarterApp.Api;

/// <summary>
/// Query to get prompts with optional filters.
/// </summary>
public record GetPromptsQuery : IRequest<IEnumerable<PromptDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; init; }

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
/// Handler for GetPromptsQuery.
/// </summary>
public class GetPromptsQueryHandler : IRequestHandler<GetPromptsQuery, IEnumerable<PromptDto>>
{
    private readonly IConversationStarterAppContext _context;
    private readonly ILogger<GetPromptsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPromptsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetPromptsQueryHandler(
        IConversationStarterAppContext context,
        ILogger<GetPromptsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PromptDto>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting prompts with filters - UserId: {UserId}, Category: {Category}, Depth: {Depth}", request.UserId, request.Category, request.Depth);

        var query = _context.Prompts.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(x => (int)x.Category == request.Category.Value);
        }

        if (request.Depth.HasValue)
        {
            query = query.Where(x => (int)x.Depth == request.Depth.Value);
        }

        var prompts = await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return prompts.Select(x => x.ToDto());
    }
}
