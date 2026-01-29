using Appliances.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appliances.Api.Features;

public record DeleteApplianceCommand(Guid ApplianceId) : IRequest<bool>;

public class DeleteApplianceCommandHandler : IRequestHandler<DeleteApplianceCommand, bool>
{
    private readonly IAppliancesDbContext _context;

    public DeleteApplianceCommandHandler(IAppliancesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteApplianceCommand request, CancellationToken cancellationToken)
    {
        var appliance = await _context.Appliances
            .FirstOrDefaultAsync(a => a.ApplianceId == request.ApplianceId, cancellationToken);

        if (appliance == null) return false;

        _context.Appliances.Remove(appliance);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
