using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.Benefits;

public record CreateBenefitCommand : IRequest<BenefitDto>
{
    public Guid UserId { get; init; }
    public Guid? CompensationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? EstimatedValue { get; init; }
    public decimal? EmployerContribution { get; init; }
    public decimal? EmployeeContribution { get; init; }
}

public class CreateBenefitCommandHandler : IRequestHandler<CreateBenefitCommand, BenefitDto>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<CreateBenefitCommandHandler> _logger;

    public CreateBenefitCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<CreateBenefitCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BenefitDto> Handle(CreateBenefitCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating benefit for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var benefit = new Benefit
        {
            BenefitId = Guid.NewGuid(),
            UserId = request.UserId,
            CompensationId = request.CompensationId,
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            EstimatedValue = request.EstimatedValue,
            EmployerContribution = request.EmployerContribution,
            EmployeeContribution = request.EmployeeContribution,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Benefits.Add(benefit);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created benefit {BenefitId} for user {UserId}",
            benefit.BenefitId,
            request.UserId);

        return benefit.ToDto();
    }
}
