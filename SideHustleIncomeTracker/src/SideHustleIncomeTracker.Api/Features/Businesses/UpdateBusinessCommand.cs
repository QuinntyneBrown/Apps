using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Businesses;

public record UpdateBusinessCommand : IRequest<BusinessDto?>
{
    public Guid BusinessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public string? TaxId { get; init; }
    public string? Notes { get; init; }
    public decimal TotalIncome { get; init; }
    public decimal TotalExpenses { get; init; }
}

public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, BusinessDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<UpdateBusinessCommandHandler> _logger;

    public UpdateBusinessCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<UpdateBusinessCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BusinessDto?> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating business {BusinessId}", request.BusinessId);

        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.BusinessId == request.BusinessId, cancellationToken);

        if (business == null)
        {
            _logger.LogWarning("Business {BusinessId} not found", request.BusinessId);
            return null;
        }

        business.Name = request.Name;
        business.Description = request.Description;
        business.IsActive = request.IsActive;
        business.TaxId = request.TaxId;
        business.Notes = request.Notes;
        business.TotalIncome = request.TotalIncome;
        business.TotalExpenses = request.TotalExpenses;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated business {BusinessId}", request.BusinessId);

        return business.ToDto();
    }
}
