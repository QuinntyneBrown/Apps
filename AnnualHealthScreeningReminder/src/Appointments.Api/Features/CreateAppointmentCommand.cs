using Appointments.Core;
using Appointments.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Appointments.Api.Features;

public record CreateAppointmentCommand(
    Guid UserId,
    Guid TenantId,
    string ProviderName,
    string AppointmentType,
    DateTime AppointmentDate,
    string? Location,
    string? Notes) : IRequest<AppointmentDto>;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
{
    private readonly IAppointmentsDbContext _context;
    private readonly ILogger<CreateAppointmentCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateAppointmentCommandHandler(
        IAppointmentsDbContext context,
        ILogger<CreateAppointmentCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment
        {
            AppointmentId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            ProviderName = request.ProviderName,
            AppointmentType = request.AppointmentType,
            AppointmentDate = request.AppointmentDate,
            Location = request.Location,
            Notes = request.Notes,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishAppointmentCreatedEventAsync(appointment);

        _logger.LogInformation("Appointment created: {AppointmentId}", appointment.AppointmentId);

        return appointment.ToDto();
    }

    private Task PublishAppointmentCreatedEventAsync(Appointment appointment)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("appointments-events", ExchangeType.Topic, durable: true);

            var @event = new AppointmentCreatedEvent
            {
                UserId = appointment.UserId,
                TenantId = appointment.TenantId,
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                ProviderName = appointment.ProviderName,
                AppointmentType = appointment.AppointmentType
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("appointments-events", "appointment.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish AppointmentCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
