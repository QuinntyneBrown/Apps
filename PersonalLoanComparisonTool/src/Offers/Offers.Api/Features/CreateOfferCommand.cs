using Offers.Core;
using Offers.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Offers.Api.Features;

public record CreateOfferCommand(
    Guid LoanId,
    Guid TenantId,
    string LenderName,
    decimal InterestRate,
    int TermMonths,
    decimal MonthlyPayment,
    decimal TotalCost,
    decimal OriginationFee,
    string? Notes) : IRequest<OfferDto>;

public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, OfferDto>
{
    private readonly IOffersDbContext _context;
    private readonly ILogger<CreateOfferCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateOfferCommandHandler(
        IOffersDbContext context,
        ILogger<CreateOfferCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<OfferDto> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = request.LoanId,
            TenantId = request.TenantId,
            LenderName = request.LenderName,
            InterestRate = request.InterestRate,
            TermMonths = request.TermMonths,
            MonthlyPayment = request.MonthlyPayment,
            TotalCost = request.TotalCost,
            OriginationFee = request.OriginationFee,
            Notes = request.Notes,
            ReceivedAt = DateTime.UtcNow
        };

        _context.Offers.Add(offer);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishOfferReceivedEventAsync(offer);

        _logger.LogInformation("Offer created: {OfferId}", offer.OfferId);

        return offer.ToDto();
    }

    private Task PublishOfferReceivedEventAsync(Offer offer)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("offers-events", ExchangeType.Topic, durable: true);

            var @event = new OfferReceivedEvent
            {
                TenantId = offer.TenantId,
                OfferId = offer.OfferId,
                LoanId = offer.LoanId,
                LenderName = offer.LenderName,
                InterestRate = offer.InterestRate
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("offers-events", "offer.received", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish OfferReceivedEvent");
        }

        return Task.CompletedTask;
    }
}
