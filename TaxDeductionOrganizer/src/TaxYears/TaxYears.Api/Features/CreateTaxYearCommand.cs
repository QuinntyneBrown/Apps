using TaxYears.Core;
using TaxYears.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace TaxYears.Api.Features;

public record CreateTaxYearCommand(Guid TenantId, Guid UserId, int Year, string? Notes) : IRequest<TaxYearDto>;

public class CreateTaxYearCommandHandler : IRequestHandler<CreateTaxYearCommand, TaxYearDto>
{
    private readonly ITaxYearsDbContext _context;
    private readonly ILogger<CreateTaxYearCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateTaxYearCommandHandler(
        ITaxYearsDbContext context,
        ILogger<CreateTaxYearCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<TaxYearDto> Handle(CreateTaxYearCommand request, CancellationToken cancellationToken)
    {
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            TenantId = request.TenantId,
            UserId = request.UserId,
            Year = request.Year,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.TaxYears.Add(taxYear);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishTaxYearCreatedEventAsync(taxYear);

        _logger.LogInformation("TaxYear created: {TaxYearId}", taxYear.TaxYearId);

        return taxYear.ToDto();
    }

    private Task PublishTaxYearCreatedEventAsync(TaxYear taxYear)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("taxyears-events", ExchangeType.Topic, durable: true);

            var @event = new TaxYearCreatedEvent
            {
                UserId = taxYear.UserId,
                TenantId = taxYear.TenantId,
                TaxYearId = taxYear.TaxYearId,
                Year = taxYear.Year
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("taxyears-events", "taxyear.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish TaxYearCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
