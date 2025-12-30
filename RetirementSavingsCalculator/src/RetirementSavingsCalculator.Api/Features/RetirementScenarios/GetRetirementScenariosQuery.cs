using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Api.Features.RetirementScenarios;

public record GetRetirementScenariosQuery : IRequest<IEnumerable<RetirementScenarioDto>>
{
    public int? MinCurrentAge { get; init; }
    public int? MaxCurrentAge { get; init; }
    public int? MinRetirementAge { get; init; }
    public int? MaxRetirementAge { get; init; }
    public decimal? MinCurrentSavings { get; init; }
    public decimal? MaxCurrentSavings { get; init; }
}

public class GetRetirementScenariosQueryHandler : IRequestHandler<GetRetirementScenariosQuery, IEnumerable<RetirementScenarioDto>>
{
    private readonly IRetirementSavingsCalculatorContext _context;
    private readonly ILogger<GetRetirementScenariosQueryHandler> _logger;

    public GetRetirementScenariosQueryHandler(
        IRetirementSavingsCalculatorContext context,
        ILogger<GetRetirementScenariosQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<RetirementScenarioDto>> Handle(GetRetirementScenariosQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting retirement scenarios");

        var query = _context.RetirementScenarios.AsNoTracking();

        if (request.MinCurrentAge.HasValue)
        {
            query = query.Where(s => s.CurrentAge >= request.MinCurrentAge.Value);
        }

        if (request.MaxCurrentAge.HasValue)
        {
            query = query.Where(s => s.CurrentAge <= request.MaxCurrentAge.Value);
        }

        if (request.MinRetirementAge.HasValue)
        {
            query = query.Where(s => s.RetirementAge >= request.MinRetirementAge.Value);
        }

        if (request.MaxRetirementAge.HasValue)
        {
            query = query.Where(s => s.RetirementAge <= request.MaxRetirementAge.Value);
        }

        if (request.MinCurrentSavings.HasValue)
        {
            query = query.Where(s => s.CurrentSavings >= request.MinCurrentSavings.Value);
        }

        if (request.MaxCurrentSavings.HasValue)
        {
            query = query.Where(s => s.CurrentSavings <= request.MaxCurrentSavings.Value);
        }

        var scenarios = await query
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        return scenarios.Select(s => s.ToDto());
    }
}
