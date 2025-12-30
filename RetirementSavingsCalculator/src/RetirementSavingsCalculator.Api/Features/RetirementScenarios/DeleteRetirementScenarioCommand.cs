using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.RetirementScenarios;

public record DeleteRetirementScenarioCommand : IRequest<bool>
{
    public Guid RetirementScenarioId { get; init; }
}

public class DeleteRetirementScenarioCommandHandler : IRequestHandler<DeleteRetirementScenarioCommand, bool>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<DeleteRetirementScenarioCommandHandler> _logger;

    public DeleteRetirementScenarioCommandHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<DeleteRetirementScenarioCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRetirementScenarioCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting retirement scenario {RetirementScenarioId}", request.RetirementScenarioId);

        var scenario = await _context.RetirementScenarios
            .FirstOrDefaultAsync(s => s.RetirementScenarioId == request.RetirementScenarioId, cancellationToken);

        if (scenario == null)
        {
            _logger.LogWarning("Retirement scenario {RetirementScenarioId} not found", request.RetirementScenarioId);
            return false;
        }

        _context.RetirementScenarios.Remove(scenario);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted retirement scenario {RetirementScenarioId}", request.RetirementScenarioId);

        return true;
    }
}
