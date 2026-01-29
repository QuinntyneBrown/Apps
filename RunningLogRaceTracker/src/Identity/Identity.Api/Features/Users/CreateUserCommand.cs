using Identity.Core;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record CreateUserCommand : IRequest<UserDto>
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Guid? TenantId { get; init; }
}

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
        var tenantId = request.TenantId ?? Guid.NewGuid();

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);

        var user = new User(tenantId, request.UserName, request.Email, hashedPassword, salt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}
