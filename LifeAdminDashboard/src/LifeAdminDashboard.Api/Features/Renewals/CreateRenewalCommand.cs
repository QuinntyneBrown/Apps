using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Renewals;

public record CreateRenewalCommand : IRequest<RenewalDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string RenewalType { get; init; } = string.Empty;
    public string? Provider { get; init; }
    public DateTime RenewalDate { get; init; }
    public decimal? Cost { get; init; }
    public string Frequency { get; init; } = string.Empty;
    public bool IsAutoRenewal { get; init; }
    public string? Notes { get; init; }
}

public class CreateRenewalCommandHandler : IRequestHandler<CreateRenewalCommand, RenewalDto>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<CreateRenewalCommandHandler> _logger;

    public CreateRenewalCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<CreateRenewalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RenewalDto> Handle(CreateRenewalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating renewal for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var renewal = new Renewal
        {
            RenewalId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            RenewalType = request.RenewalType,
            Provider = request.Provider,
            RenewalDate = request.RenewalDate,
            Cost = request.Cost,
            Frequency = request.Frequency,
            IsAutoRenewal = request.IsAutoRenewal,
            IsActive = true,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Renewals.Add(renewal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created renewal {RenewalId} for user {UserId}",
            renewal.RenewalId,
            request.UserId);

        return renewal.ToDto();
    }
}
