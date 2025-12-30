using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Renewals;

public record UpdateRenewalCommand : IRequest<RenewalDto?>
{
    public Guid RenewalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string RenewalType { get; init; } = string.Empty;
    public string? Provider { get; init; }
    public DateTime RenewalDate { get; init; }
    public decimal? Cost { get; init; }
    public string Frequency { get; init; } = string.Empty;
    public bool IsAutoRenewal { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public class UpdateRenewalCommandHandler : IRequestHandler<UpdateRenewalCommand, RenewalDto?>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<UpdateRenewalCommandHandler> _logger;

    public UpdateRenewalCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<UpdateRenewalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RenewalDto?> Handle(UpdateRenewalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating renewal {RenewalId}", request.RenewalId);

        var renewal = await _context.Renewals
            .FirstOrDefaultAsync(r => r.RenewalId == request.RenewalId, cancellationToken);

        if (renewal == null)
        {
            _logger.LogWarning("Renewal {RenewalId} not found", request.RenewalId);
            return null;
        }

        renewal.Name = request.Name;
        renewal.RenewalType = request.RenewalType;
        renewal.Provider = request.Provider;
        renewal.RenewalDate = request.RenewalDate;
        renewal.Cost = request.Cost;
        renewal.Frequency = request.Frequency;
        renewal.IsAutoRenewal = request.IsAutoRenewal;
        renewal.IsActive = request.IsActive;
        renewal.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated renewal {RenewalId}", request.RenewalId);

        return renewal.ToDto();
    }
}
