using Identity.Core;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Services;
using MediatR;

namespace Identity.Api.Features.Users;

public record CreateUserCommand(Guid TenantId, string UserName, string Email, string Password) : IRequest<UserDto>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IIdentityDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var (hash, salt) = _passwordHasher.HashPassword(request.Password);
        var user = new User(request.TenantId, request.UserName, request.Email, hash, salt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto(user.UserId, user.TenantId, user.UserName, user.Email, Enumerable.Empty<string>());
    }
}
