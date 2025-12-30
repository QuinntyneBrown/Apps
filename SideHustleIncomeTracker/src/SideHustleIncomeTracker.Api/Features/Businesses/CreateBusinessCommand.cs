using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Businesses;

public record CreateBusinessCommand : IRequest<BusinessDto>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public string? TaxId { get; init; }
    public string? Notes { get; init; }
}

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BusinessDto>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<CreateBusinessCommandHandler> _logger;

    public CreateBusinessCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<CreateBusinessCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BusinessDto> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating business: {BusinessName}", request.Name);

        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            IsActive = true,
            TaxId = request.TaxId,
            Notes = request.Notes,
            TotalIncome = 0m,
            TotalExpenses = 0m,
        };

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created business {BusinessId}: {BusinessName}", business.BusinessId, business.Name);

        return business.ToDto();
    }
}
