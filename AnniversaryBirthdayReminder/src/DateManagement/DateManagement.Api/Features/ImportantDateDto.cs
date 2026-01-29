using DateManagement.Core.Models;

namespace DateManagement.Api.Features;

public record ImportantDateDto(
    Guid ImportantDateId,
    Guid UserId,
    string Title,
    DateTime Date,
    string? Description,
    int DaysBeforeReminder,
    bool IsActive,
    DateTime CreatedAt);

public static class ImportantDateExtensions
{
    public static ImportantDateDto ToDto(this ImportantDate date)
    {
        return new ImportantDateDto(
            date.ImportantDateId,
            date.UserId,
            date.Title,
            date.Date,
            date.Description,
            date.DaysBeforeReminder,
            date.IsActive,
            date.CreatedAt);
    }
}
