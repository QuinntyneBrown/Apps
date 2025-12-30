using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Compensations;

public record CreateCompensationCommand : IRequest<CompensationDto>
{
    public Guid UserId { get; init; }
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

public class CreateCompensationCommandHandler : IRequestHandler<CreateCompensationCommand, CompensationDto>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<CreateCompensationCommandHandler> _logger;

    public CreateCompensationCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<CreateCompensationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CompensationDto> Handle(CreateCompensationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating compensation for user {UserId}, employer: {Employer}",
            request.UserId,
            request.Employer);

        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = request.UserId,
            CompensationType = request.CompensationType,
            Employer = request.Employer,
            JobTitle = request.JobTitle,
            BaseSalary = request.BaseSalary,
            Currency = request.Currency,
            Bonus = request.Bonus,
            StockValue = request.StockValue,
            OtherCompensation = request.OtherCompensation,
            EffectiveDate = request.EffectiveDate,
            EndDate = request.EndDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        compensation.CalculateTotalCompensation();

        _context.Compensations.Add(compensation);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created compensation {CompensationId} for user {UserId}",
            compensation.CompensationId,
            request.UserId);

        return compensation.ToDto();
    }
}
