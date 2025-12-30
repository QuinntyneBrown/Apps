using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Compensations;

public record UpdateCompensationCommand : IRequest<CompensationDto?>
{
    public Guid CompensationId { get; init; }
    public CompensationType CompensationType { get; init; }
    public string Employer { get; init; } = string.Empty;
    public string JobTitle { get; init; } = string.Empty;
    public decimal BaseSalary { get; init; }
    public string Currency { get; init; } = "USD";
    public decimal? Bonus { get; init; }
    public decimal? StockValue { get; init; }
    public decimal? OtherCompensation { get; init; }
    public DateTime EffectiveDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Notes { get; init; }
}

public class UpdateCompensationCommandHandler : IRequestHandler<UpdateCompensationCommand, CompensationDto?>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<UpdateCompensationCommandHandler> _logger;

    public UpdateCompensationCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<UpdateCompensationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompensationDto?> Handle(UpdateCompensationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating compensation {CompensationId}", request.CompensationId);

        var compensation = await _context.Compensations
            .FirstOrDefaultAsync(c => c.CompensationId == request.CompensationId, cancellationToken);

        if (compensation == null)
        {
            _logger.LogWarning("Compensation {CompensationId} not found", request.CompensationId);
            return null;
        }

        compensation.CompensationType = request.CompensationType;
        compensation.Employer = request.Employer;
        compensation.JobTitle = request.JobTitle;
        compensation.BaseSalary = request.BaseSalary;
        compensation.Currency = request.Currency;
        compensation.Bonus = request.Bonus;
        compensation.StockValue = request.StockValue;
        compensation.OtherCompensation = request.OtherCompensation;
        compensation.EffectiveDate = request.EffectiveDate;
        compensation.EndDate = request.EndDate;
        compensation.Notes = request.Notes;

        compensation.CalculateTotalCompensation();

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated compensation {CompensationId}", request.CompensationId);

        return compensation.ToDto();
    }
}
