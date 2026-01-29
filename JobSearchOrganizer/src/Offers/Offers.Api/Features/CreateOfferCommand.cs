using Offers.Core;
using Offers.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Offers.Api.Features;

public record CreateOfferCommand(
    Guid UserId,
    Guid TenantId,
    Guid ApplicationId,
    decimal SalaryAmount,
    string? SalaryType,
    decimal? SigningBonus,
    decimal? AnnualBonus,
    string? Benefits,
    DateTime StartDate,
    DateTime? ExpirationDate,
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
            UserId = request.UserId,
            TenantId = request.TenantId,
            ApplicationId = request.ApplicationId,
            SalaryAmount = request.SalaryAmount,
            SalaryType = request.SalaryType,
            SigningBonus = request.SigningBonus,
            AnnualBonus = request.AnnualBonus,
            Benefits = request.Benefits,
            StartDate = request.StartDate,
            ExpirationDate = request.ExpirationDate,
            Status = OfferStatus.Pending,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
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
                UserId = offer.UserId,
                TenantId = offer.TenantId,
                OfferId = offer.OfferId,
                ApplicationId = offer.ApplicationId,
                SalaryAmount = offer.SalaryAmount
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
