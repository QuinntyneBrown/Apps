using Identity.Core;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record CreateUserCommand : IRequest<UserDto>
{
    public Guid TenantId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
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
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var (hash, salt) = _passwordHasher.HashPassword(request.Password);
        var user = new User(request.TenantId, request.UserName, request.Email, hash, salt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = new List<string>()
        };
    }
}
