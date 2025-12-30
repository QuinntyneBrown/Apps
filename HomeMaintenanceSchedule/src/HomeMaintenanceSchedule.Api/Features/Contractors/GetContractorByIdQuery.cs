using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.Contractors;

public record GetContractorByIdQuery : IRequest<ContractorDto?>
{
    public Guid ContractorId { get; init; }
}

public class GetContractorByIdQueryHandler : IRequestHandler<GetContractorByIdQuery, ContractorDto?>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<GetContractorByIdQueryHandler> _logger;

    public GetContractorByIdQueryHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<GetContractorByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContractorDto?> Handle(GetContractorByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contractor {ContractorId}", request.ContractorId);

        var contractor = await _context.Contractors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ContractorId == request.ContractorId, cancellationToken);

        return contractor?.ToDto();
    }
}
