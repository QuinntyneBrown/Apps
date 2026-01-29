using Companies.Core;
using Companies.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Companies.Api.Features;

public record CreateCompanyCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    string? Website,
    string? Industry,
    string? Location,
    string? Size,
    string? Description,
    string? Notes,
    int? Rating) : IRequest<CompanyDto>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly ICompaniesDbContext _context;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateCompanyCommandHandler(
        ICompaniesDbContext context,
        ILogger<CreateCompanyCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            Website = request.Website,
            Industry = request.Industry,
            Location = request.Location,
            Size = request.Size,
            Description = request.Description,
            Notes = request.Notes,
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishCompanyAddedEventAsync(company);

        _logger.LogInformation("Company created: {CompanyId}", company.CompanyId);

        return company.ToDto();
    }

    private Task PublishCompanyAddedEventAsync(Company company)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("companies-events", ExchangeType.Topic, durable: true);

            var @event = new CompanyAddedEvent
            {
                UserId = company.UserId,
                TenantId = company.TenantId,
                CompanyId = company.CompanyId,
                Name = company.Name
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("companies-events", "company.added", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish CompanyAddedEvent");
        }

        return Task.CompletedTask;
    }
}
