using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Prompts;

public record DeletePromptCommand : IRequest<bool>
{
    public Guid PromptId { get; init; }
}

public class DeletePromptCommandHandler : IRequestHandler<DeletePromptCommand, bool>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<DeletePromptCommandHandler> _logger;

    public DeletePromptCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<DeletePromptCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePromptCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting prompt {PromptId}", request.PromptId);

        var prompt = await _context.Prompts
            .FirstOrDefaultAsync(p => p.PromptId == request.PromptId, cancellationToken);

        if (prompt == null)
        {
            _logger.LogWarning("Prompt {PromptId} not found", request.PromptId);
            return false;
        }

        if (prompt.IsSystemPrompt)
        {
            _logger.LogWarning("Cannot delete system prompt {PromptId}", request.PromptId);
            return false;
        }

        _context.Prompts.Remove(prompt);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted prompt {PromptId}", request.PromptId);

        return true;
    }
}
