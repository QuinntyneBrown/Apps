using DailyJournalingApp.Core;

namespace DailyJournalingApp.Api.Features.Prompts;

public record PromptDto
{
    public Guid PromptId { get; init; }
    public string Text { get; init; } = string.Empty;
    public string? Category { get; init; }
    public bool IsSystemPrompt { get; init; }
    public Guid? CreatedByUserId { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PromptExtensions
{
    public static PromptDto ToDto(this Prompt prompt)
    {
        return new PromptDto
        {
            PromptId = prompt.PromptId,
            Text = prompt.Text,
            Category = prompt.Category,
            IsSystemPrompt = prompt.IsSystemPrompt,
            CreatedByUserId = prompt.CreatedByUserId,
            CreatedAt = prompt.CreatedAt,
        };
    }
}
