using Identity.Core;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Identity.Api.Features.Users;

public record CreateUserCommand : IRequest<UserDto>
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Guid TenantId { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateUserCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        ILogger<CreateUserCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Email == request.Email, cancellationToken);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this username or email already exists");
        }

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);
        var user = new User(request.TenantId, request.UserName, request.Email, hashedPassword, salt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        // Publish event
        await PublishUserCreatedEventAsync(user, cancellationToken);

        _logger.LogInformation("User created: {UserId}", user.UserId);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = new List<RoleInfo>()
        };
    }

    private async Task PublishUserCreatedEventAsync(User user, CancellationToken cancellationToken)
    {
        if (_rabbitConnection == null) return;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("identity-events", ExchangeType.Topic, durable: true);

            var @event = new UserCreatedEvent
            {
                UserId = user.UserId,
                TenantId = user.TenantId,
                Username = user.UserName,
                Email = user.Email
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("identity-events", "user.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish UserCreatedEvent");
        }
    }
}
