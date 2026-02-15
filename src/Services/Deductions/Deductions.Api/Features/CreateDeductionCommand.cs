using Deductions.Core;
using Deductions.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Deductions.Api.Features;

public record CreateDeductionCommand(
    Guid TenantId,
    Guid UserId,
    Guid TaxYearId,
    string Description,
    decimal Amount,
    DateTime Date,
    DeductionCategory Category,
    string? Notes) : IRequest<DeductionDto>;

public class CreateDeductionCommandHandler : IRequestHandler<CreateDeductionCommand, DeductionDto>
{
    private readonly IDeductionsDbContext _context;
    private readonly ILogger<CreateDeductionCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateDeductionCommandHandler(
        IDeductionsDbContext context,
        ILogger<CreateDeductionCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<DeductionDto> Handle(CreateDeductionCommand request, CancellationToken cancellationToken)
    {
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TenantId = request.TenantId,
            UserId = request.UserId,
            TaxYearId = request.TaxYearId,
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            Category = request.Category,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Deductions.Add(deduction);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishDeductionAddedEventAsync(deduction);

        _logger.LogInformation("Deduction created: {DeductionId}", deduction.DeductionId);

        return deduction.ToDto();
    }

    private Task PublishDeductionAddedEventAsync(Deduction deduction)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("deductions-events", ExchangeType.Topic, durable: true);

            var @event = new DeductionAddedEvent
            {
                UserId = deduction.UserId,
                TenantId = deduction.TenantId,
                DeductionId = deduction.DeductionId,
                TaxYearId = deduction.TaxYearId,
                Amount = deduction.Amount,
                Category = deduction.Category.ToString()
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("deductions-events", "deduction.added", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish DeductionAddedEvent");
        }

        return Task.CompletedTask;
    }
}
