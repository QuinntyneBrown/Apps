using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.RetirementScenarios;

public record GetRetirementScenarioByIdQuery : IRequest<RetirementScenarioDto?>
{
    public Guid RetirementScenarioId { get; init; }
}

public class GetRetirementScenarioByIdQueryHandler : IRequestHandler<GetRetirementScenarioByIdQuery, RetirementScenarioDto?>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<GetRetirementScenarioByIdQueryHandler> _logger;

    public GetRetirementScenarioByIdQueryHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<GetRetirementScenarioByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RetirementScenarioDto?> Handle(GetRetirementScenarioByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting retirement scenario {RetirementScenarioId}", request.RetirementScenarioId);

        var scenario = await _context.RetirementScenarios
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.RetirementScenarioId == request.RetirementScenarioId, cancellationToken);

        if (scenario == null)
        {
            _logger.LogWarning("Retirement scenario {RetirementScenarioId} not found", request.RetirementScenarioId);
            return null;
        }

        return scenario.ToDto();
    }
}
