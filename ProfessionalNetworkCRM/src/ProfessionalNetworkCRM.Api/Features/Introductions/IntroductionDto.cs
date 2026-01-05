using ProfessionalNetworkCRM.Core.Model.IntroductionAggregate;
using ProfessionalNetworkCRM.Core.Model.IntroductionAggregate.Enums;

namespace ProfessionalNetworkCRM.Api.Features.Introductions;

public record IntroductionDto
{
    public Guid IntroductionId { get; init; }
    public Guid FromContactId { get; init; }
    public Guid ToContactId { get; init; }
    public string Purpose { get; init; } = string.Empty;
    public IntroductionStatus Status { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class IntroductionExtensions
{
    public static IntroductionDto ToDto(this Introduction introduction)
    {
        return new IntroductionDto
        {
            IntroductionId = introduction.IntroductionId,
            FromContactId = introduction.FromContactId,
            ToContactId = introduction.ToContactId,
            Purpose = introduction.Purpose,
            Status = introduction.Status,
            Notes = introduction.Notes,
            CreatedAt = introduction.CreatedAt,
            UpdatedAt = introduction.UpdatedAt,
        };
    }
}
