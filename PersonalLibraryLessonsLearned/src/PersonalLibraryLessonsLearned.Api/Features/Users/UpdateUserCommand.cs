using PersonalLibraryLessonsLearned.Core;
using PersonalLibraryLessonsLearned.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalLibraryLessonsLearned.Api.Features.Users;

public record UpdateUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IPersonalLibraryLessonsLearnedContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IPersonalLibraryLessonsLearnedContext context, IPasswordHasher passwordHasher, ILogger<UpdateUserCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user {UserId}", request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;

        if (!string.IsNullOrEmpty(request.UserName) && request.UserName != user.UserName)
            if (await _context.Users.AnyAsync(u => u.UserName == request.UserName && u.UserId != request.UserId, cancellationToken))
                throw new InvalidOperationException($"Username '{request.UserName}' is already taken.");

        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email)
            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.UserId != request.UserId, cancellationToken))
                throw new InvalidOperationException($"Email '{request.Email}' is already registered.");

        user.UpdateProfile(request.UserName, request.Email);
        if (!string.IsNullOrEmpty(request.Password))
        {
            var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);
            user.SetPassword(hashedPassword, salt);
        }
        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }
}
