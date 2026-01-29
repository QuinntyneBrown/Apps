using Identity.Core;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Services;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Identity.Api.Features.Users;

public record RegisterUserCommand(
    Guid TenantId,
    string UserName,
    string Email,
    string Password) : IRequest<UserDto>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public RegisterUserCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        ILogger<RegisterUserCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (hash, salt) = _passwordHasher.HashPassword(request.Password);
        var user = new User(request.TenantId, request.UserName, request.Email, hash, salt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishUserCreatedEventAsync(user);

        _logger.LogInformation("User created: {UserId}", user.UserId);

        return new UserDto(user.UserId, user.TenantId, user.UserName, user.Email, Array.Empty<string>());
    }

    private Task PublishUserCreatedEventAsync(User user)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

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

        return Task.CompletedTask;
    }
}
