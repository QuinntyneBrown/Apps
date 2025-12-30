using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;

public record DeleteInsuranceInfoCommand : IRequest<bool>
{
    public Guid InsuranceInfoId { get; init; }
}

public class DeleteInsuranceInfoCommandHandler : IRequestHandler<DeleteInsuranceInfoCommand, bool>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<DeleteInsuranceInfoCommandHandler> _logger;

    public DeleteInsuranceInfoCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<DeleteInsuranceInfoCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteInsuranceInfoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting insurance info {InsuranceInfoId}", request.InsuranceInfoId);

        var insuranceInfo = await _context.InsuranceInfos
            .FirstOrDefaultAsync(i => i.InsuranceInfoId == request.InsuranceInfoId, cancellationToken);

        if (insuranceInfo == null)
        {
            _logger.LogWarning("Insurance info {InsuranceInfoId} not found", request.InsuranceInfoId);
            return false;
        }

        _context.InsuranceInfos.Remove(insuranceInfo);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted insurance info {InsuranceInfoId}", request.InsuranceInfoId);

        return true;
    }
}
