namespace Identity.Api.Features.Users;

public record UserDto(
    Guid UserId,
    Guid TenantId,
    string UserName,
    string Email,
    IEnumerable<string> Roles);
