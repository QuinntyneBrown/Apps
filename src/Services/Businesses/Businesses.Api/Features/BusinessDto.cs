namespace Businesses.Api.Features;

public record BusinessDto(
    Guid BusinessId,
    Guid TenantId,
    Guid UserId,
    string Name,
    string? Description,
    string? Category,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
