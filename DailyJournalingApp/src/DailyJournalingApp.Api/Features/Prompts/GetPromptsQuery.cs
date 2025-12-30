using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Prompts;

public record GetPromptsQuery : IRequest<IEnumerable<PromptDto>>
{
    public string? Category { get; init; }
    public bool? SystemPromptsOnly { get; init; }
    public Guid? UserId { get; init; }
}

public class GetPromptsQueryHandler : IRequestHandler<GetPromptsQuery, IEnumerable<PromptDto>>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<GetPromptsQueryHandler> _logger;

    public GetPromptsQueryHandler(
        IDailyJournalingAppContext context,
        ILogger<GetPromptsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PromptDto>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting prompts with category {Category}", request.Category);

        var query = _context.Prompts.AsNoTracking();

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(p => p.Category == request.Category);
        }

        if (request.SystemPromptsOnly == true)
        {
            query = query.Where(p => p.IsSystemPrompt);
        }
        else if (request.UserId.HasValue)
        {
            query = query.Where(p => p.IsSystemPrompt || p.CreatedByUserId == request.UserId.Value);
        }

        var prompts = await query
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Text)
            .ToListAsync(cancellationToken);

        return prompts.Select(p => p.ToDto());
    }
}
