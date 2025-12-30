using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Benefits;

public record UpdateBenefitCommand : IRequest<BenefitDto?>
{
    public Guid BenefitId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? EstimatedValue { get; init; }
    public decimal? EmployerContribution { get; init; }
    public decimal? EmployeeContribution { get; init; }
}

public class UpdateBenefitCommandHandler : IRequestHandler<UpdateBenefitCommand, BenefitDto?>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<UpdateBenefitCommandHandler> _logger;

    public UpdateBenefitCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<UpdateBenefitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BenefitDto?> Handle(UpdateBenefitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating benefit {BenefitId}", request.BenefitId);

        var benefit = await _context.Benefits
            .FirstOrDefaultAsync(b => b.BenefitId == request.BenefitId, cancellationToken);

        if (benefit == null)
        {
            _logger.LogWarning("Benefit {BenefitId} not found", request.BenefitId);
            return null;
        }

        benefit.Name = request.Name;
        benefit.Category = request.Category;
        benefit.Description = request.Description;
        benefit.EstimatedValue = request.EstimatedValue;
        benefit.EmployerContribution = request.EmployerContribution;
        benefit.EmployeeContribution = request.EmployeeContribution;
        benefit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated benefit {BenefitId}", request.BenefitId);

        return benefit.ToDto();
    }
}
