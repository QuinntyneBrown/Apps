using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.Contractors;

public record GetContractorsQuery : IRequest<IEnumerable<ContractorDto>>
{
    public Guid? UserId { get; init; }
    public string? Specialty { get; init; }
    public bool? IsActive { get; init; }
}

public class GetContractorsQueryHandler : IRequestHandler<GetContractorsQuery, IEnumerable<ContractorDto>>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<GetContractorsQueryHandler> _logger;

    public GetContractorsQueryHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<GetContractorsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ContractorDto>> Handle(GetContractorsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contractors for user {UserId}", request.UserId);

        var query = _context.Contractors.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.Specialty))
        {
            query = query.Where(c => c.Specialty == request.Specialty);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }

        var contractors = await query
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return contractors.Select(c => c.ToDto());
    }
}
