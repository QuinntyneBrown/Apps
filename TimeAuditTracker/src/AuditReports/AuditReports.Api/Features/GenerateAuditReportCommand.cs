using AuditReports.Core;
using AuditReports.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace AuditReports.Api.Features;

public record GenerateAuditReportCommand(
    Guid TenantId,
    Guid UserId,
    DateTime StartDate,
    DateTime EndDate,
    int TotalMinutes,
    int ProductiveMinutes,
    string? Summary) : IRequest<AuditReportDto>;

public class GenerateAuditReportCommandHandler : IRequestHandler<GenerateAuditReportCommand, AuditReportDto>
{
    private readonly IAuditReportsDbContext _context;
    private readonly ILogger<GenerateAuditReportCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public GenerateAuditReportCommandHandler(
        IAuditReportsDbContext context,
        ILogger<GenerateAuditReportCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<AuditReportDto> Handle(GenerateAuditReportCommand request, CancellationToken cancellationToken)
    {
        var report = new AuditReport
        {
            ReportId = Guid.NewGuid(),
            TenantId = request.TenantId,
            UserId = request.UserId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalMinutes = request.TotalMinutes,
            ProductiveMinutes = request.ProductiveMinutes,
            Summary = request.Summary,
            GeneratedAt = DateTime.UtcNow
        };

        _context.AuditReports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishAuditReportGeneratedEventAsync(report);

        _logger.LogInformation("AuditReport generated: {ReportId}", report.ReportId);

        return report.ToDto();
    }

    private Task PublishAuditReportGeneratedEventAsync(AuditReport report)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("auditreports-events", ExchangeType.Topic, durable: true);

            var @event = new AuditReportGeneratedEvent
            {
                UserId = report.UserId,
                TenantId = report.TenantId,
                ReportId = report.ReportId,
                StartDate = report.StartDate,
                EndDate = report.EndDate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("auditreports-events", "auditreport.generated", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish AuditReportGeneratedEvent");
        }

        return Task.CompletedTask;
    }
}
