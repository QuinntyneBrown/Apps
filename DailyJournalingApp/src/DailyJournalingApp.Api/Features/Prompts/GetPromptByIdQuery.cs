using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Prompts;

public record GetPromptByIdQuery : IRequest<PromptDto?>
{
    public Guid PromptId { get; init; }
}

public class GetPromptByIdQueryHandler : IRequestHandler<GetPromptByIdQuery, PromptDto?>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<GetPromptByIdQueryHandler> _logger;

    public GetPromptByIdQueryHandler(
        IDailyJournalingAppContext context,
        ILogger<GetPromptByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PromptDto?> Handle(GetPromptByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting prompt {PromptId}", request.PromptId);

        var prompt = await _context.Prompts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PromptId == request.PromptId, cancellationToken);

        return prompt?.ToDto();
    }
}
