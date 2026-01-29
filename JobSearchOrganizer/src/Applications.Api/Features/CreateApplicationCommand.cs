using Applications.Core;
using Applications.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Applications.Api.Features;

public record CreateApplicationCommand(
    Guid UserId,
    Guid TenantId,
    Guid CompanyId,
    string JobTitle,
    string? JobDescription,
    string? JobUrl,
    decimal? SalaryMin,
    decimal? SalaryMax,
    string? Notes) : IRequest<ApplicationDto>;

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, ApplicationDto>
{
    private readonly IApplicationsDbContext _context;
    private readonly ILogger<CreateApplicationCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateApplicationCommandHandler(
        IApplicationsDbContext context,
        ILogger<CreateApplicationCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ApplicationDto> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = new Application
        {
            ApplicationId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            CompanyId = request.CompanyId,
            JobTitle = request.JobTitle,
            JobDescription = request.JobDescription,
            JobUrl = request.JobUrl,
            Status = ApplicationStatus.Applied,
            AppliedDate = DateTime.UtcNow,
            SalaryMin = request.SalaryMin,
            SalaryMax = request.SalaryMax,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishApplicationSubmittedEventAsync(application);

        _logger.LogInformation("Application created: {ApplicationId}", application.ApplicationId);

        return application.ToDto();
    }

    private Task PublishApplicationSubmittedEventAsync(Application application)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("applications-events", ExchangeType.Topic, durable: true);

            var @event = new ApplicationSubmittedEvent
            {
                UserId = application.UserId,
                TenantId = application.TenantId,
                ApplicationId = application.ApplicationId,
                CompanyId = application.CompanyId,
                JobTitle = application.JobTitle
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("applications-events", "application.submitted", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish ApplicationSubmittedEvent");
        }

        return Task.CompletedTask;
    }
}
