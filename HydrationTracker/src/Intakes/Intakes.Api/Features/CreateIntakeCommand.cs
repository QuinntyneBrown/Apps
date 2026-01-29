using Intakes.Core;
using Intakes.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Intakes.Api.Features;

public record CreateIntakeCommand(
    Guid UserId,
    Guid TenantId,
    BeverageType BeverageType,
    decimal AmountMl,
    DateTime LoggedAt,
    string? Notes) : IRequest<IntakeDto>;

public class CreateIntakeCommandHandler : IRequestHandler<CreateIntakeCommand, IntakeDto>
{
    private readonly IIntakesDbContext _context;
    private readonly ILogger<CreateIntakeCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateIntakeCommandHandler(
        IIntakesDbContext context,
        ILogger<CreateIntakeCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<IntakeDto> Handle(CreateIntakeCommand request, CancellationToken cancellationToken)
    {
        var intake = new Intake
        {
            IntakeId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            BeverageType = request.BeverageType,
            AmountMl = request.AmountMl,
            LoggedAt = request.LoggedAt,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Intakes.Add(intake);
        await _context.SaveChangesAsync(cancellationToken);

        // Publish event
        await PublishIntakeLoggedEventAsync(intake);

        _logger.LogInformation("Intake logged: {IntakeId}", intake.IntakeId);

        return intake.ToDto();
    }

    private Task PublishIntakeLoggedEventAsync(Intake intake)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("intakes-events", ExchangeType.Topic, durable: true);

            var @event = new IntakeLoggedEvent
            {
                UserId = intake.UserId,
                TenantId = intake.TenantId,
                IntakeId = intake.IntakeId,
                AmountMl = (int)intake.AmountMl,
                BeverageType = intake.BeverageType.ToString(),
                LoggedAt = intake.LoggedAt
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("intakes-events", "intake.logged", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish IntakeLoggedEvent");
        }

        return Task.CompletedTask;
    }
}
