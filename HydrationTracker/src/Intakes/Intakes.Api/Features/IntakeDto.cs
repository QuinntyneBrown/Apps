using Intakes.Core.Models;

namespace Intakes.Api.Features;

public record IntakeDto(
    Guid IntakeId,
    Guid UserId,
    BeverageType BeverageType,
    decimal AmountMl,
    DateTime LoggedAt,
    string? Notes,
    DateTime CreatedAt);

public static class IntakeExtensions
{
    public static IntakeDto ToDto(this Intake intake)
    {
        return new IntakeDto(
            intake.IntakeId,
            intake.UserId,
            intake.BeverageType,
            intake.AmountMl,
            intake.LoggedAt,
            intake.Notes,
            intake.CreatedAt);
    }
}
