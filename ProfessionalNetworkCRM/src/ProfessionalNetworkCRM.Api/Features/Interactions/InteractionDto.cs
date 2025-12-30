using ProfessionalNetworkCRM.Core;

namespace ProfessionalNetworkCRM.Api.Features.Interactions;

public record InteractionDto
{
    public Guid InteractionId { get; init; }
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string InteractionType { get; init; } = string.Empty;
    public DateTime InteractionDate { get; init; }
    public string? Subject { get; init; }
    public string? Notes { get; init; }
    public string? Outcome { get; init; }
    public int? DurationMinutes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class InteractionExtensions
{
    public static InteractionDto ToDto(this Interaction interaction)
    {
        return new InteractionDto
        {
            InteractionId = interaction.InteractionId,
            UserId = interaction.UserId,
            ContactId = interaction.ContactId,
            InteractionType = interaction.InteractionType,
            InteractionDate = interaction.InteractionDate,
            Subject = interaction.Subject,
            Notes = interaction.Notes,
            Outcome = interaction.Outcome,
            DurationMinutes = interaction.DurationMinutes,
            CreatedAt = interaction.CreatedAt,
            UpdatedAt = interaction.UpdatedAt,
        };
    }
}
