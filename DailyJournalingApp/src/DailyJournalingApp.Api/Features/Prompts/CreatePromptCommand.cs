using DailyJournalingApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Prompts;

public record CreatePromptCommand : IRequest<PromptDto>
{
    public string Text { get; init; } = string.Empty;
    public string? Category { get; init; }
    public Guid? CreatedByUserId { get; init; }
}

public class CreatePromptCommandHandler : IRequestHandler<CreatePromptCommand, PromptDto>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<CreatePromptCommandHandler> _logger;

    public CreatePromptCommandHandler(
        IDailyJournalingAppContext context,
        ILogger<CreatePromptCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PromptDto> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating prompt for user {UserId}, category: {Category}",
            request.CreatedByUserId,
            request.Category);

        var prompt = new Prompt
        {
            PromptId = Guid.NewGuid(),
            Text = request.Text,
            Category = request.Category,
            IsSystemPrompt = !request.CreatedByUserId.HasValue,
            CreatedByUserId = request.CreatedByUserId,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created prompt {PromptId}", prompt.PromptId);

        return prompt.ToDto();
    }
}
