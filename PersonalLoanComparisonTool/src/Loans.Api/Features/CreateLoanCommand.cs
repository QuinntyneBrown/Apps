using Loans.Core;
using Loans.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Loans.Api.Features;

public record CreateLoanCommand(
    Guid UserId,
    Guid TenantId,
    string Name,
    string LoanType,
    decimal RequestedAmount,
    string Purpose,
    int CreditScore,
    string? Notes) : IRequest<LoanDto>;

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanDto>
{
    private readonly ILoansDbContext _context;
    private readonly ILogger<CreateLoanCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateLoanCommandHandler(
        ILoansDbContext context,
        ILogger<CreateLoanCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<LoanDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Name = request.Name,
            LoanType = request.LoanType,
            RequestedAmount = request.RequestedAmount,
            Purpose = request.Purpose,
            CreditScore = request.CreditScore,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishLoanCreatedEventAsync(loan);

        _logger.LogInformation("Loan created: {LoanId}", loan.LoanId);

        return loan.ToDto();
    }

    private Task PublishLoanCreatedEventAsync(Loan loan)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("loans-events", ExchangeType.Topic, durable: true);

            var @event = new LoanCreatedEvent
            {
                UserId = loan.UserId,
                TenantId = loan.TenantId,
                LoanId = loan.LoanId,
                Name = loan.Name,
                LoanType = loan.LoanType,
                RequestedAmount = loan.RequestedAmount
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("loans-events", "loan.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish LoanCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
